using Microsoft.AspNetCore.Mvc;
using GameSpace.Areas.MiniGame.Services;
using GameSpace.Areas.MiniGame.Models;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    public class AdminHomeController : Controller
    {
        private readonly IMiniGameAdminService _adminService;

        public AdminHomeController(IMiniGameAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var signInSummary = await _adminService.GetSignInSummaryAsync();
            var gameSummary = await _adminService.GetGameSummaryAsync();

            ViewBag.SignInSummary = signInSummary;
            ViewBag.GameSummary = gameSummary;

            return View();
        }
    }
}
