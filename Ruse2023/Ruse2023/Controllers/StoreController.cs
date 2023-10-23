using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ruse2023.Models.Store;
using Ruse2023.Services.StoreService;

namespace Ruse2023.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService storeService;
        public StoreController(IStoreService storeService)
        {
            this.storeService = storeService;
        }
        public IActionResult Index()
        {
            return View();
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
    }
}
