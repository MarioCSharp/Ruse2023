using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ruse2023.Data;
using Ruse2023.Data.Models;
using Ruse2023.Models;
using Ruse2023.Models.Store;
using Ruse2023.Services.AccountService;
using Ruse2023.Services.StoreService;

namespace Ruse2023.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService storeService;
        private readonly IAccountService accountService;
        private readonly ApplicationDbContext context;
        public StoreController(IStoreService storeService,
                               IAccountService accountService,
                               ApplicationDbContext context)
        {
            this.storeService = storeService;
            this.context = context;
            this.accountService = accountService;
        }
        public async Task<IActionResult> Index()
        {
            var credits = await context.Credits.FirstOrDefaultAsync(x => x.UserId == accountService.GetUserId());

            if (credits == null)
            {
                var c = new Credits()
                {
                    UserId = accountService.GetUserId(),
                    Ammount = 0
                };

                await context.Credits.AddAsync(c);
                await context.SaveChangesAsync();
            }

            return View(new StoreModel(){
                Products = await storeService.GetAllProducts(), Credits = context.Credits.FirstOrDefaultAsync(x => x.UserId == accountService.GetUserId()).Result.Ammount
            });
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
