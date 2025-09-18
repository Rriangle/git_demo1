using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameSpace.Areas.MiniGame.Services;
using GameSpace.Areas.MiniGame.Models;
using GameSpace.Areas.MiniGame.Filters;

namespace GameSpace.Areas.MiniGame.Controllers
{
    [Area("MiniGame")]
    [Authorize(AuthenticationSchemes = "AdminCookie")]
    [MiniGameModulePermission("Pet")]
    public class AdminPetController : Controller
    {
        private readonly IMiniGameAdminService _adminService;

        public AdminPetController(IMiniGameAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index(PetQueryModel query)
        {
            if (query.PageNumber <= 0) query.PageNumber = 1;
            if (query.PageNumber <= 0) query.PageNumber = 10;

            var pets = await _adminService.GetPetsAsync(query);
            var petSummary = await _adminService.GetPetSummaryAsync();

            var viewModel = new AdminPetIndexViewModel
            {
                Pets = pets,
                PetSummary = petSummary,
                Query = query
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int petId)
        {
            var petDetail = await _adminService.GetPetDetailAsync(petId);

            var viewModel = new AdminPetDetailsViewModel
            {
                PetDetail = petDetail ?? new PetDetailReadModel()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Statistics()
        {
            var petSummary = await _adminService.GetPetSummaryAsync();
            var pets = await _adminService.GetPetsAsync(new PetQueryModel { PageNumber = 10 });

            var viewModel = new AdminPetStatisticsViewModel
            {
                Summary = petSummary,
                TopPets = pets.Items.ToList()
            };

            return View(viewModel);
        }
    }
}
