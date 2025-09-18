using GameSpace.Models;
using GameSpace.Areas.MiniGame.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameSpace.Areas.MiniGame.Services
{
    public class MiniGameAdminService : IMiniGameAdminService
    {
        private readonly GameSpacedatabaseContext _context;
        private readonly ILogger<MiniGameAdminService> _logger;

        public MiniGameAdminService(GameSpacedatabaseContext context, ILogger<MiniGameAdminService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // 會員點數系統相關方法
        public async Task<PagedResult<WalletReadModel>> GetUserWalletsAsync(WalletQueryModel query)
        {
            var queryable = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                queryable = queryable.Where(u => u.UserName.Contains(query.SearchTerm) || u.UserAccount.Contains(query.SearchTerm));
            }

            var totalCount = await queryable.CountAsync();
            var users = await queryable
                .Skip((query.PageNumber - 1) * query.PageNumberSize)
                .Take(query.PageNumberSize)
                .Select(u => new WalletReadModel
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    UserPoint = 0, // User 模型沒有 UserPoint 屬性，需要從其他地方獲取
                    LastUpdated = DateTime.Now // User 模型沒有 LastUpdated 屬性
                })
                .ToListAsync();

            return new PagedResult<WalletReadModel>
            {
                Items = users,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageNumberSize
            };
        }

        public async Task<WalletSummaryReadModel> GetWalletSummaryAsync()
        {
            var totalUsers = await _context.Users.CountAsync();
            var totalPoints = 0; // 需要從其他地方計算總點數
            var averagePoints = totalUsers > 0 ? totalPoints / totalUsers : 0;

            return new WalletSummaryReadModel
            {
                TotalUsers = totalUsers,
                TotalPoints = totalPoints,
                AveragePoints = averagePoints
            };
        }

        public async Task<WalletDetailReadModel> GetWalletDetailAsync(int userId)
        {
            var user = await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => new WalletDetailReadModel
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    UserPoint = 0, // User 模型沒有 UserPoint 屬性
                    LastUpdated = DateTime.Now // User 模型沒有 LastUpdated 屬性
                })
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var transactions = await _context.WalletHistories
                    .Where(w => w.UserId == userId)
                    .OrderByDescending(w => w.ChangeTime)
                    .Select(w => new WalletTransactionReadModel
                    {
                        TransactionId = w.LogId,
                        UserId = w.UserId,
                        Amount = w.PointsChanged,
                        TransactionType = w.ChangeType,
                        Description = w.Description ?? "",
                        TransactionDate = w.ChangeTime
                    })
                    .ToListAsync();

                user.Transactions = transactions;
            }

            return user ?? new WalletDetailReadModel();
        }

        public async Task<bool> AdjustUserPointsAsync(int userId, int pointsChange, string description)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null) return false;

                // 創建錢包歷史記錄
                var transaction = new GameSpace.Models.WalletHistory
                {
                    UserId = userId,
                    PointsChanged = pointsChange,
                    ChangeType = pointsChange > 0 ? "增加" : "減少",
                    Description = description,
                    ChangeTime = DateTime.Now
                };

                _context.WalletHistories.Add(transaction);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "調整用戶點數失敗: UserId={UserId}, PointsChange={PointsChange}", userId, pointsChange);
                return false;
            }
        }

        public async Task<PagedResult<UserCouponReadModel>> QueryUserCouponsAsync(CouponQueryModel query)
        {
            var queryable = _context.Coupons
                .Include(c => c.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                queryable = queryable.Where(c => c.User.UserName.Contains(query.SearchTerm));
            }

            var totalCount = await queryable.CountAsync();
            var coupons = await queryable
                .Skip((query.PageNumber - 1) * query.PageNumberSize)
                .Take(query.PageNumberSize)
                .Select(c => new UserCouponReadModel
                {
                    UserCouponId = c.CouponId,
                    UserId = c.UserId,
                    UserName = c.User.UserName,
                    CouponId = c.CouponTypeId,
                    CouponName = "", // 需要從 CouponType 獲取
                    IssuedDate = c.AcquiredTime,
                    UsedDate = c.UsedTime,
                    IsUsed = c.IsUsed
                })
                .ToListAsync();

            return new PagedResult<UserCouponReadModel>
            {
                Items = coupons,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageNumberSize
            };
        }

        public async Task<bool> IssueCouponToUserAsync(int userId, int couponTypeId, int quantity)
        {
            try
            {
                var couponType = await _context.CouponTypes.FindAsync(couponTypeId);
                if (couponType == null) return false;

                for (int i = 0; i < quantity; i++)
                {
                    var coupon = new GameSpace.Models.Coupon
                    {
                        UserId = userId,
                        CouponTypeId = couponTypeId,
                        CouponCode = GenerateCouponCode(),
                        AcquiredTime = DateTime.Now,
                        IsUsed = false
                    };
                    _context.Coupons.Add(coupon);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "發放優惠券失敗: UserId={UserId}, CouponTypeId={CouponTypeId}, Quantity={Quantity}", userId, couponTypeId, quantity);
                return false;
            }
        }

        public async Task<bool> RemoveUserCouponAsync(int userCouponId)
        {
            try
            {
                var coupon = await _context.Coupons.FindAsync(userCouponId);
                if (coupon == null) return false;

                _context.Coupons.Remove(coupon);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "移除優惠券失敗: UserCouponId={UserCouponId}", userCouponId);
                return false;
            }
        }

        public async Task<PagedResult<UserEVoucherReadModel>> QueryUserEVouchersAsync(EVoucherQueryModel query)
        {
            var queryable = _context.Evouchers
                .Include(e => e.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                queryable = queryable.Where(e => e.User.UserName.Contains(query.SearchTerm));
            }

            var totalCount = await queryable.CountAsync();
            var eVouchers = await queryable
                .Skip((query.PageNumber - 1) * query.PageNumberSize)
                .Take(query.PageNumberSize)
                .Select(e => new UserEVoucherReadModel
                {
                    UserEVoucherId = e.EvoucherId,
                    UserId = e.UserId,
                    EVoucherId = e.EvoucherTypeId,
                    EVoucherName = "", // 需要從 EVoucherType 獲取
                    IssuedDate = e.AcquiredTime,
                    UsedDate = e.UsedTime,
                    IsUsed = e.IsUsed
                })
                .ToListAsync();

            return new PagedResult<UserEVoucherReadModel>
            {
                Items = eVouchers,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageNumberSize
            };
        }

        public async Task<bool> IssueEVoucherToUserAsync(int userId, int eVoucherTypeId, int quantity)
        {
            try
            {
                var eVoucherType = await _context.EvoucherTypes.FindAsync(eVoucherTypeId);
                if (eVoucherType == null) return false;

                for (int i = 0; i < quantity; i++)
                {
                    var eVoucher = new GameSpace.Models.Evoucher
                    {
                        UserId = userId,
                        EvoucherTypeId = eVoucherTypeId,
                        EvoucherCode = GenerateEVoucherCode(),
                        AcquiredTime = DateTime.Now,
                        IsUsed = false
                    };
                    _context.Evouchers.Add(eVoucher);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "發放電子禮券失敗: UserId={UserId}, EVoucherTypeId={EVoucherTypeId}, Quantity={Quantity}", userId, eVoucherTypeId, quantity);
                return false;
            }
        }

        public async Task<bool> RemoveUserEVoucherAsync(int userEVoucherId)
        {
            try
            {
                var eVoucher = await _context.Evouchers.FindAsync(userEVoucherId);
                if (eVoucher == null) return false;

                _context.Evouchers.Remove(eVoucher);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "移除電子禮券失敗: UserEVoucherId={UserEVoucherId}", userEVoucherId);
                return false;
            }
        }

        public async Task<PagedResult<WalletTransactionReadModel>> GetWalletHistoryAsync(WalletQueryModel query)
        {
            var queryable = _context.WalletHistories
                .Include(w => w.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                queryable = queryable.Where(w => w.User.UserName.Contains(query.SearchTerm));
            }

            var totalCount = await queryable.CountAsync();
            var transactions = await queryable
                .OrderByDescending(w => w.ChangeTime)
                .Skip((query.PageNumber - 1) * query.PageNumberSize)
                .Take(query.PageNumberSize)
                .Select(w => new WalletTransactionReadModel
                {
                    TransactionId = w.LogId,
                    UserId = w.UserId,
                    Amount = w.PointsChanged,
                    TransactionType = w.ChangeType,
                    Description = w.Description ?? "",
                    TransactionDate = w.ChangeTime
                })
                .ToListAsync();

            return new PagedResult<WalletTransactionReadModel>
            {
                Items = transactions,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageNumberSize
            };
        }

        // 會員簽到系統相關方法
        public async Task<SignInRuleReadModel> GetSignInRuleAsync()
        {
            // 這裡應該從規則表中讀取，暫時返回預設值
            return new SignInRuleReadModel
            {
                RuleId = 1,
                PointsPerSignIn = 10,
                ConsecutiveBonus = 5,
                MaxConsecutiveDays = 7
            };
        }

        public async Task<bool> UpdateSignInRuleAsync(SignInRuleUpdateModel model)
        {
            try
            {
                // 這裡應該更新規則表，暫時返回成功
                await Task.Delay(100); // 模擬異步操作
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新簽到規則失敗");
                return false;
            }
        }

        public async Task<PagedResult<SignInStatsReadModel>> GetSignInStatsAsync(SignInQueryModel query)
        {
            var queryable = _context.UserSignInStats
                .Include(s => s.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                queryable = queryable.Where(s => s.User.UserName.Contains(query.SearchTerm));
            }

            var totalCount = await queryable.CountAsync();
            var stats = await queryable
                .Skip((query.PageNumber - 1) * query.PageNumberSize)
                .Take(query.PageNumberSize)
                .Select(s => new SignInStatsReadModel
                {
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    SignInCount = 1, // UserSignInStat 沒有 SignInCount 屬性
                    LastSignIn = s.SignTime
                })
                .ToListAsync();

            return new PagedResult<SignInStatsReadModel>
            {
                Items = stats,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageNumberSize
            };
        }

        public async Task<IEnumerable<UserSignInHistoryReadModel>> GetUserSignInHistoryAsync(int userId)
        {
            var history = await _context.UserSignInStats
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.SignTime)
                .Select(s => new UserSignInHistoryReadModel
                {
                    UserId = s.UserId,
                    SignInDate = s.SignTime,
                    PointsEarned = s.PointsGained
                })
                .ToListAsync();

            return history;
        }

        public async Task<bool> AddUserSignInRecordAsync(int userId, DateTime signInDate)
        {
            try
            {
                var signInRecord = new GameSpace.Models.UserSignInStat
                {
                    UserId = userId,
                    SignTime = signInDate,
                    PointsGained = 10,
                    PointsGainedTime = signInDate,
                    ExpGained = 5,
                    ExpGainedTime = signInDate,
                    CouponGained = "",
                    CouponGainedTime = signInDate
                };

                _context.UserSignInStats.Add(signInRecord);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "新增簽到記錄失敗: UserId={UserId}, SignInDate={SignInDate}", userId, signInDate);
                return false;
            }
        }

        public async Task<bool> RemoveUserSignInRecordAsync(int userId, DateTime signInDate)
        {
            try
            {
                var signInRecord = await _context.UserSignInStats
                    .FirstOrDefaultAsync(s => s.UserId == userId && s.SignTime.Date == signInDate.Date);

                if (signInRecord != null)
                {
                    _context.UserSignInStats.Remove(signInRecord);
                    await _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "移除簽到記錄失敗: UserId={UserId}, SignInDate={SignInDate}", userId, signInDate);
                return false;
            }
        }

        // 寵物系統相關方法
        public async Task<PetRuleReadModel> GetPetRuleAsync()
        {
            // 這裡應該從規則表中讀取，暫時返回預設值
            return new PetRuleReadModel
            {
                RuleId = 1,
                PointsPerLevelUp = 100,
                PointsPerSkinChange = 50,
                PointsPerBackgroundChange = 30
            };
        }

        public async Task<bool> UpdatePetRuleAsync(PetRuleUpdateModel model)
        {
            try
            {
                // 這裡應該更新規則表，暫時返回成功
                await Task.Delay(100); // 模擬異步操作
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新寵物規則失敗");
                return false;
            }
        }

        public async Task<PagedResult<PetReadModel>> GetPetsAsync(PetQueryModel query)
        {
            var queryable = _context.Pets
                .Include(p => p.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                queryable = queryable.Where(p => p.User.UserName.Contains(query.SearchTerm) || p.PetName.Contains(query.SearchTerm));
            }

            var totalCount = await queryable.CountAsync();
            var pets = await queryable
                .Skip((query.PageNumber - 1) * query.PageNumberSize)
                .Take(query.PageNumberSize)
                .Select(p => new PetReadModel
                {
                    PetId = p.PetId,
                    UserId = p.UserId,
                    UserName = p.User.UserName,
                    PetName = p.PetName,
                    Level = p.Level,
                    Experience = p.Experience
                })
                .ToListAsync();

            return new PagedResult<PetReadModel>
            {
                Items = pets,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageNumberSize
            };
        }

        public async Task<PetSummaryReadModel> GetPetSummaryAsync()
        {
            var totalPets = await _context.Pets.CountAsync();
            var activePets = await _context.Pets.CountAsync(p => p.Level > 0);
            var averageLevel = totalPets > 0 ? await _context.Pets.AverageAsync(p => p.Level) : 0;

            return new PetSummaryReadModel
            {
                TotalPets = totalPets,
                ActivePets = activePets,
                AverageLevel = (int)averageLevel
            };
        }

        public async Task<PetDetailReadModel> GetPetDetailAsync(int petId)
        {
            var pet = await _context.Pets
                .Include(p => p.User)
                .Where(p => p.PetId == petId)
                .Select(p => new PetDetailReadModel
                {
                    PetId = p.PetId,
                    UserId = p.UserId,
                    UserName = p.User.UserName,
                    PetName = p.PetName,
                    Level = p.Level,
                    Experience = p.Experience,
                    SkinColor = p.SkinColor,
                    BackgroundColor = p.BackgroundColor
                })
                .FirstOrDefaultAsync();

            return pet ?? new PetDetailReadModel();
        }

        public async Task<bool> UpdatePetDetailsAsync(int petId, PetUpdateModel model)
        {
            try
            {
                var pet = await _context.Pets.FindAsync(petId);
                if (pet == null) return false;

                pet.PetName = model.PetName;
                pet.Level = model.Level;
                pet.Experience = model.Experience;
                pet.SkinColor = model.SkinColor;
                pet.BackgroundColor = model.BackgroundColor;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新寵物資料失敗: PetId={PetId}", petId);
                return false;
            }
        }

        public async Task<PagedResult<PetSkinColorChangeLogReadModel>> GetPetSkinColorChangeLogsAsync(PetQueryModel query)
        {
            // 這裡應該從日誌表中讀取，暫時返回空結果
            return new PagedResult<PetSkinColorChangeLogReadModel>
            {
                Items = new List<PetSkinColorChangeLogReadModel>(),
                TotalCount = 0,
                PageNumber = query.PageNumber,
                PageSize = query.PageNumberSize
            };
        }

        public async Task<PagedResult<PetBackgroundColorChangeLogReadModel>> GetPetBackgroundColorChangeLogsAsync(PetQueryModel query)
        {
            // 這裡應該從日誌表中讀取，暫時返回空結果
            return new PagedResult<PetBackgroundColorChangeLogReadModel>
            {
                Items = new List<PetBackgroundColorChangeLogReadModel>(),
                TotalCount = 0,
                PageNumber = query.PageNumber,
                PageSize = query.PageNumberSize
            };
        }

        // 小遊戲系統相關方法
        public async Task<GameRuleReadModel> GetGameRuleAsync()
        {
            // 這裡應該從規則表中讀取，暫時返回預設值
            return new GameRuleReadModel
            {
                RuleId = 1,
                MonsterCountPerLevel = 5,
                MonsterSpeedMultiplier = 1.0m,
                PointsPerWin = 20,
                DailyPlayLimit = 3
            };
        }

        public async Task<bool> UpdateGameRuleAsync(GameRuleUpdateModel model)
        {
            try
            {
                // 這裡應該更新規則表，暫時返回成功
                await Task.Delay(100); // 模擬異步操作
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新遊戲規則失敗");
                return false;
            }
        }

        public async Task<PagedResult<GameRecordReadModel>> GetGameRecordsAsync(GameQueryModel query)
        {
            var queryable = _context.MiniGames
                .Include(m => m.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                queryable = queryable.Where(m => m.User.UserName.Contains(query.SearchTerm));
            }

            var totalCount = await queryable.CountAsync();
            var records = await queryable
                .OrderByDescending(m => m.StartTime)
                .Skip((query.PageNumber - 1) * query.PageNumberSize)
                .Take(query.PageNumberSize)
                .Select(m => new GameRecordReadModel
                {
                    PlayId = m.PlayId,
                    UserId = m.UserId,
                    UserName = m.User.UserName,
                    Score = m.PointsGained, // 使用 PointsGained 作為分數
                    PlayDate = m.StartTime
                })
                .ToListAsync();

            return new PagedResult<GameRecordReadModel>
            {
                Items = records,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageNumberSize
            };
        }

        public async Task<GameSummaryReadModel> GetGameSummaryAsync()
        {
            var totalPlays = await _context.MiniGames.CountAsync();
            var totalUsers = await _context.MiniGames.Select(m => m.UserId).Distinct().CountAsync();
            var averageScore = totalPlays > 0 ? await _context.MiniGames.AverageAsync(m => m.PointsGained) : 0;

            return new GameSummaryReadModel
            {
                TotalPlays = totalPlays,
                TotalUsers = totalUsers,
                AverageScore = (int)averageScore
            };
        }

        public async Task<GameDetailReadModel> GetGameDetailAsync(int gameId)
        {
            var game = await _context.MiniGames
                .Include(m => m.User)
                .Where(m => m.PlayId == gameId)
                .Select(m => new GameDetailReadModel
                {
                    PlayId = m.PlayId,
                    UserId = m.UserId,
                    UserName = m.User.UserName,
                    Score = m.PointsGained,
                    PlayDate = m.StartTime,
                    GameData = m.Result ?? ""
                })
                .FirstOrDefaultAsync();

            return game ?? new GameDetailReadModel();
        }

        // 輔助方法
        private string GenerateCouponCode()
        {
            return "CPN" + DateTime.Now.Ticks.ToString("X")[^8..];
        }

        private string GenerateEVoucherCode()
        {
            return "EV" + DateTime.Now.Ticks.ToString("X")[^10..];
        }
    }
}
