using GameSpace.Areas.MiniGame.Models;
using GameSpace.Areas.MiniGame.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    [Authorize(Policy = "CanPet")] // Requires Pet permission
    public class AdminPetController : Controller
    {
        private readonly IMiniGameAdminService _adminService;
        private readonly IMiniGameAdminAuthService _authService;

        public AdminPetController(IMiniGameAdminService adminService, IMiniGameAdminAuthService authService)
        {
            _adminService = adminService;
            _authService = authService;
        }

        public async Task<IActionResult> Index(PetQueryModel query)
        {
            var pets = await _adminService.GetPetsAsync(query);
            return View(pets);
        }

        public async Task<IActionResult> SetRule()
        {
            var rule = await _adminService.GetPetRuleAsync();
            return View(rule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetRule(PetRuleUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _adminService.UpdatePetRuleAsync(model);
                if (success)
                {
                    TempData["SuccessMessage"] = "寵物規則更新成功";
                    return RedirectToAction("SetRule");
                }
                else
                {
                    TempData["ErrorMessage"] = "寵物規則更新失敗";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Detail(int petId)
        {
            var pet = await _adminService.GetPetDetailAsync(petId);
            if (pet == null || pet.PetId == 0)
            {
                return NotFound();
            }
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int petId, PetUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _adminService.UpdatePetDetailsAsync(petId, model);
                if (success)
                {
                    TempData["SuccessMessage"] = "寵物資料更新成功";
                    return RedirectToAction("Detail", new { petId });
                }
                else
                {
                    TempData["ErrorMessage"] = "寵物資料更新失敗";
                }
            }
            return RedirectToAction("Detail", new { petId });
        }

        public async Task<IActionResult> SkinColorChangeLogs(PetQueryModel query)
        {
            var logs = await _adminService.GetPetSkinColorChangeLogsAsync(query);
            return View(logs);
        }

        public async Task<IActionResult> BackgroundColorChangeLogs(PetQueryModel query)
        {
            var logs = await _adminService.GetPetBackgroundColorChangeLogsAsync(query);
            return View(logs);
        }
    }
}
