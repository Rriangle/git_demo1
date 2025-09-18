using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameSpace.Areas.MiniGame.Services;
using GameSpace.Areas.MiniGame.Models;
using GameSpace.Areas.MiniGame.Filters;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    [MiniGameModulePermission("MiniGame")]
    public class AdminMiniGameController : Controller
    {
        private readonly IMiniGameAdminService _adminService;

        public AdminMiniGameController(IMiniGameAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index(GameQueryModel query)
        {
            if (query.PageNumber <= 0) query.PageNumber = 1;
            if (query.PageNumber <= 0) query.PageNumber = 10;

            var gameRecords = await _adminService.GetGameRecordsAsync(query);
            var gameSummary = await _adminService.GetGameSummaryAsync();

            var viewModel = new AdminMiniGameIndexViewModel
            {
                GameRecords = gameRecords,
                GameSummary = gameSummary,
                Query = query
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int playId)
        {
            var gameDetail = await _adminService.GetGameDetailAsync(playId);
            var gameSummary = await _adminService.GetGameSummaryAsync();

            var viewModel = new AdminMiniGameDetailsViewModel
            {
                GameDetail = gameDetail ?? new GameDetailReadModel()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Statistics()
        {
            var gameSummary = await _adminService.GetGameSummaryAsync();
            var gameRecords = await _adminService.GetGameRecordsAsync(new GameQueryModel { PageNumber = 10 });

            var viewModel = new AdminMiniGameStatisticsViewModel
            {
                Summary = gameSummary,
                RecentGames = gameRecords.Items.ToList()
            };

            return View(viewModel);
        }
    }
}
