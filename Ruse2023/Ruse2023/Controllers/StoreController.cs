using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ruse2023.Models.Store;
using Ruse2023.Services.AccountService;
using Ruse2023.Services.StoreService;

namespace Ruse2023.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService storeService;
        private readonly IAccountService accountService;
        public StoreController(IStoreService storeService,
                               IAccountService accountService)
        {
            this.storeService = storeService;
            this.accountService = accountService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await storeService.GetAllProducts());
        }
        [Authorize(Policy = "AdministratorModeratorPolicy")]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "AdministratorModeratorPolicy")]
        public async Task<IActionResult> AddProduct(ProductModel model, List<IFormFile> Image)
        {
            if (!ModelState.IsValid && model.Image != null)
            {
                return View(model);
            }

            var res = await storeService.AddProduct(model, Image);

            if (!res) return Unauthorized();

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> ProductById(int id)
        {
            return View(await storeService.ProductById(id));
        }
        [Authorize]
        public async Task<IActionResult> BuyProduct(int id)
        {
            var result = await storeService.BuyProduct(id, accountService.GetUserId());

            if (!result) return Unauthorized();

            return RedirectToAction("Index", "Home");
        }
    }
}
