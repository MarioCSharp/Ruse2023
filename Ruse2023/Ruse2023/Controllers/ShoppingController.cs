using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ruse2023.Models.Shopping;
using Ruse2023.Services.AccountService;
using Ruse2023.Services.ShoppingService;

namespace Ruse2023.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IShoppingService shoppingService;
        private readonly IAccountService accountService;
        public ShoppingController(IShoppingService shoppingService,
                                  IAccountService accountService)
        {
            this.shoppingService = shoppingService;
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
        public async Task<IActionResult> Create(ShoppingApplicationModel model, List<IFormFile> Image)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await shoppingService.Create(model, Image, this.accountService.GetUserId());

            if (!result) return Unauthorized();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Policy = "AdministratorModeratorPolicy")]
        public async Task<IActionResult> Applications()
        {
            return View(await shoppingService.GetAll());
        }

        [HttpGet]
        [Authorize(Policy = "AdministratorModeratorPolicy")]
        public async Task<IActionResult> Details(int id)
        {
            return View(await shoppingService.GetDetails(id));
        }

        [HttpPost]
        [Authorize(Policy = "AdministratorModeratorPolicy")]
        public async Task<IActionResult> Details(ShoppingApprovalModel model)
        {
            await shoppingService.GiveCredits(model);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Policy = "AdministratorModeratorPolicy")]
        public async Task<IActionResult> Decline(int id)
        {
            await shoppingService.Decline(id);

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> MyApplications()
        {
            return View(await shoppingService.GetMyApplications(accountService.GetUserId()));
        }
        [Authorize]
        public async Task<IActionResult> ApplicationDetails(int id)
        {
            return View(await shoppingService.GetDetails(id));
        }
    }
}
