using GameSpace.Areas.MiniGame.Models;
using GameSpace.Areas.MiniGame.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    [Authorize(Policy = "CanAdmin")] // Requires Admin permission
    public class AdminMiniGameController : Controller
    {
        private readonly IMiniGameAdminService _adminService;
        private readonly IMiniGameAdminAuthService _authService;

        public AdminMiniGameController(IMiniGameAdminService adminService, IMiniGameAdminAuthService authService)
        {
            _adminService = adminService;
            _authService = authService;
        }

        public async Task<IActionResult> Index(GameQueryModel query)
        {
            var records = await _adminService.GetGameRecordsAsync(query);
            return View(records);
        }

        public async Task<IActionResult> SetRule()
        {
            var rule = await _adminService.GetGameRuleAsync();
            return View(rule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetRule(GameRuleUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _adminService.UpdateGameRuleAsync(model);
                if (success)
                {
                    TempData["SuccessMessage"] = "遊戲規則更新成功";
                    return RedirectToAction("SetRule");
                }
                else
                {
                    TempData["ErrorMessage"] = "遊戲規則更新失敗";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Detail(int gameId)
        {
            var game = await _adminService.GetGameDetailAsync(gameId);
            if (game == null || game.PlayId == 0)
            {
                return NotFound();
            }
            return View(game);
        }
    }
}
