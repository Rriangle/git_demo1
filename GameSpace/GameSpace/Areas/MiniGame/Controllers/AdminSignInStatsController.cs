using GameSpace.Areas.MiniGame.Models;
using GameSpace.Areas.MiniGame.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    public class AdminSignInStatsController : Controller
    {
        private readonly IMiniGameAdminService _adminService;

        public AdminSignInStatsController(IMiniGameAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index(SignInQueryModel query)
        {
            var signInStats = await _adminService.GetSignInStatsAsync(query);
            var signInSummary = await _adminService.GetSignInSummaryAsync();
            
            var model = new SignInStatsViewModel
            {
                SignInStats = signInStats,
                Summary = signInSummary,
                Query = query ?? new SignInQueryModel()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleSignIn(SignInToggleModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _adminService.ToggleSignInAsync(model.UserId, model.Action == "add");
                
                if (success)
                {
                    TempData["SuccessMessage"] = $"簽到記錄{(model.Action == "add" ? "新增" : "移除")}成功";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = $"簽到記錄{(model.Action == "add" ? "新增" : "移除")}失敗";
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> BulkToggleSignIn(BulkSignInToggleModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _adminService.BulkToggleSignInAsync(model.UserIds, model.Action == "add");
                
                if (success)
                {
                    TempData["SuccessMessage"] = $"批量{(model.Action == "add" ? "新增" : "移除")}簽到記錄成功";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = $"批量{(model.Action == "add" ? "新增" : "移除")}簽到記錄失敗";
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
