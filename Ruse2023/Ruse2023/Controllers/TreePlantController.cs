using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ruse2023.Models.TreePlant;
using Ruse2023.Services.AccountService;
using Ruse2023.Services.TreePlantService;

namespace Ruse2023.Controllers
{
    public class TreePlantController : Controller
    {
        private readonly ITreePlantService treePlantService;
        private readonly IAccountService accountService;
        public TreePlantController(ITreePlantService treePlantService,
                                   IAccountService accountService)
        {
            this.treePlantService = treePlantService;
            this.accountService = accountService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(TreePlantApplicationModel model, List<IFormFile> Image)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await treePlantService.CreateApplication(model, Image, accountService.GetUserId());

            if (!result) return RedirectToAction("Error", "Home");

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize(Policy = "AdministratorModeratorPolicy")]
        public async Task<IActionResult> Applications()
        {
            return View(await treePlantService.GetAll());
        }
        [HttpGet]
        [Authorize(Policy = "AdministratorModeratorPolicy")]
        public async Task<IActionResult> Details(int id)
        {
            return View(await treePlantService.GetDetails(id));
        }

        [HttpPost]
        [Authorize(Policy = "AdministratorModeratorPolicy")]
        public async Task<IActionResult> Details(TreePlantApprovalModel model)
        {
            await treePlantService.GiveCredits(model);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Policy = "AdministratorModeratorPolicy")]
        public async Task<IActionResult> Decline(int id)
        {
            await treePlantService.Decline(id);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ApplicationDetails(int id)
        {
            return View(await treePlantService.GetDetails(id));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyApplications()
        {
            return View(await treePlantService.GetMyApplications(accountService.GetUserId()));
        }
    }
}
