using Microsoft.AspNetCore.Mvc;
using GameSpace.Areas.MiniGame.Services;
using GameSpace.Areas.MiniGame.Models;
using GameSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    public class MiniGameController : Controller
    {
        private readonly GameSpacedatabaseContext _context;
        private readonly ILogger<MiniGameController> _logger;

        public MiniGameController(GameSpacedatabaseContext context, ILogger<MiniGameController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = GetCurrentUserId();
                
                // 取得最近的遊戲記錄
                var games = await _context.MiniGames
                    .Include(g => g.User)
                    .Where(g => g.Result == "Completed")
                    .OrderByDescending(g => g.StartTime)
                    .Take(10)
                    .ToListAsync();

                var userWallet = await _context.UserWallets
                    .FirstOrDefaultAsync(w => w.UserId == userId);

                var viewModel = new MiniGameIndexViewModel
                {
                    MiniGames = games,
                    Pet = new Pet(),
                    Wallet = userWallet ?? new UserWallet(),
                    CanPlay = true,
                    TodayGames = 0,
                    RemainingGames = 5,
                    SenderID = userId
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得小遊戲頁面時發生錯誤");
                return View(new MiniGameIndexViewModel());
            }
        }

        public async Task<IActionResult> Play()
        {
            try
            {
                var userId = GetCurrentUserId();
                
                // 取得用戶的寵物
                var pet = await _context.Pets
                    .FirstOrDefaultAsync(p => p.UserId == userId);

                var userWallet = await _context.UserWallets
                    .FirstOrDefaultAsync(w => w.UserId == userId);

                var viewModel = new MiniGamePlayViewModel
                {
                    Pet = pet ?? new Pet(),
                    Wallet = userWallet ?? new UserWallet(),
                    SenderID = userId,
                    Difficulty = 1
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得遊戲頁面時發生錯誤");
                return View(new MiniGamePlayViewModel());
            }
        }

        [HttpPost]
        public async Task<IActionResult> StartGame(int petId, int level, int monsterCount, decimal speedMultiplier)
        {
            try
            {
                var userId = GetCurrentUserId();
                
                // 創建遊戲記錄
                var game = new GameSpace.Models.MiniGame
                {
                    UserId = userId,
                    PetId = petId,
                    Level = level,
                    MonsterCount = monsterCount,
                    SpeedMultiplier = speedMultiplier,
                    Result = "InProgress",
                    StartTime = DateTime.Now,
                    Aborted = false
                };

                _context.MiniGames.Add(game);
                await _context.SaveChangesAsync();

                return Json(new { 
                    success = true, 
                    playId = game.PlayId,
                    message = "遊戲開始！"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "開始遊戲時發生錯誤");
                return Json(new { success = false, message = "開始遊戲失敗" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EndGame(int playId, string result, int score, int expGained, int pointsGained)
        {
            try
            {
                var userId = GetCurrentUserId();
                var game = await _context.MiniGames
                    .FirstOrDefaultAsync(g => g.PlayId == playId && g.UserId == userId);

                if (game == null)
                {
                    return Json(new { success = false, message = "遊戲記錄不存在" });
                }

                // 更新遊戲結果
                game.Result = result;
                game.EndTime = DateTime.Now;
                game.ExpGained = expGained;
                game.PointsGained = pointsGained;

                // 如果遊戲成功，更新用戶錢包
                if (result == "Completed")
                {
                    var wallet = await _context.UserWallets
                        .FirstOrDefaultAsync(w => w.UserId == userId);
                    
                    if (wallet != null)
                    {
                        wallet.UserPoint += pointsGained;
                    }
                }

                await _context.SaveChangesAsync();

                return Json(new { 
                    success = true, 
                    message = "遊戲結束！",
                    finalScore = score,
                    expGained = expGained,
                    pointsGained = pointsGained
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "結束遊戲時發生錯誤");
                return Json(new { success = false, message = "結束遊戲失敗" });
            }
        }

        private int GetCurrentUserId()
        {
            return 1;
        }
    }
}
