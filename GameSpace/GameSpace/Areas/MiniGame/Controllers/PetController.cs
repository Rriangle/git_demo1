using Microsoft.AspNetCore.Mvc;
using GameSpace.Areas.MiniGame.Services;
using GameSpace.Areas.MiniGame.Models;
using GameSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    public class PetController : Controller
    {
        private readonly GameSpacedatabaseContext _context;
        private readonly ILogger<PetController> _logger;

        public PetController(GameSpacedatabaseContext context, ILogger<PetController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = GetCurrentUserId();
                var pets = await _context.Pets
                    .Where(p => p.UserId == userId)
                    .ToListAsync();

                var userWallet = await _context.UserWallets
                    .FirstOrDefaultAsync(w => w.UserId == userId);

                var viewModel = new PetViewModels.PetIndexViewModel
                {
                    Pets = pets,
                    Wallet = userWallet ?? new UserWallet(),
                    SenderID = userId
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得寵物頁面時發生錯誤");
                return View(new PetViewModels.PetIndexViewModel());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var pet = await _context.Pets
                    .FirstOrDefaultAsync(p => p.PetId == id && p.UserId == userId);

                if (pet == null)
                {
                    return NotFound();
                }

                var userWallet = await _context.UserWallets
                    .FirstOrDefaultAsync(w => w.UserId == userId);

                var viewModel = new PetViewModels.PetDetailsViewModel
                {
                    Pet = pet,
                    Wallet = userWallet ?? new UserWallet(),
                    SenderID = userId
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "取得寵物詳情時發生錯誤");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Feed(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var pet = await _context.Pets
                    .FirstOrDefaultAsync(p => p.PetId == id && p.UserId == userId);

                if (pet == null)
                {
                    return Json(new { success = false, message = "寵物不存在" });
                }

                // 檢查是否可以餵食（飢餓度低於80且距離上次餵食超過1小時）
                var canFeed = pet.Hunger < 80 && 
                    (DateTime.Now - pet.LevelUpTime).TotalHours > 1;

                if (!canFeed)
                {
                    return Json(new { success = false, message = "寵物現在不需要餵食" });
                }

                // 增加飢餓度和心情
                pet.Hunger = Math.Min(100, pet.Hunger + 20);
                pet.Mood = Math.Min(100, pet.Mood + 10);
                pet.LevelUpTime = DateTime.Now;

                await _context.SaveChangesAsync();

                return Json(new { 
                    success = true, 
                    message = "餵食成功！",
                    hunger = pet.Hunger,
                    mood = pet.Mood
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "餵食寵物時發生錯誤");
                return Json(new { success = false, message = "餵食失敗" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Play(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var pet = await _context.Pets
                    .FirstOrDefaultAsync(p => p.PetId == id && p.UserId == userId);

                if (pet == null)
                {
                    return Json(new { success = false, message = "寵物不存在" });
                }

                // 檢查是否可以玩耍（體力大於20）
                if (pet.Stamina < 20)
                {
                    return Json(new { success = false, message = "寵物太累了，需要休息" });
                }

                // 消耗體力，增加心情和經驗
                pet.Stamina = Math.Max(0, pet.Stamina - 20);
                pet.Mood = Math.Min(100, pet.Mood + 15);
                pet.Experience += 10;

                // 檢查是否升級
                var requiredExp = pet.Level * 100;
                if (pet.Experience >= requiredExp)
                {
                    pet.Level++;
                    pet.Experience -= requiredExp;
                    pet.LevelUpTime = DateTime.Now;
                }

                await _context.SaveChangesAsync();

                return Json(new { 
                    success = true, 
                    message = "玩耍成功！",
                    stamina = pet.Stamina,
                    mood = pet.Mood,
                    experience = pet.Experience,
                    level = pet.Level
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "與寵物玩耍時發生錯誤");
                return Json(new { success = false, message = "玩耍失敗" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(string petName)
        {
            try
            {
                var userId = GetCurrentUserId();

                // 檢查是否已有寵物
                var existingPets = await _context.Pets
                    .Where(p => p.UserId == userId)
                    .CountAsync();

                if (existingPets >= 3)
                {
                    return Json(new { success = false, message = "最多只能擁有3隻寵物" });
                }

                var pet = new Pet
                {
                    UserId = userId,
                    PetName = petName,
                    Level = 1,
                    Experience = 0,
                    Hunger = 50,
                    Mood = 50,
                    Health = 100,
                    Stamina = 100,
                    Cleanliness = 50,
                    SkinColor = "Blue",
                    BackgroundColor = "Green",
                    LevelUpTime = DateTime.Now,
                    SkinColorChangedTime = DateTime.Now,
                    BackgroundColorChangedTime = DateTime.Now,
                    PointsChangedSkinColor = 0,
                    PointsChangedBackgroundColor = 0,
                    PointsGainedLevelUp = 0,
                    PointsGainedTimeLevelUp = DateTime.Now
                };

                _context.Pets.Add(pet);
                await _context.SaveChangesAsync();

                return Json(new { 
                    success = true, 
                    message = "寵物創建成功！",
                    petId = pet.PetId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "創建寵物時發生錯誤");
                return Json(new { success = false, message = "創建失敗" });
            }
        }

        private int GetCurrentUserId()
        {
            return 1;
        }
    }
}
