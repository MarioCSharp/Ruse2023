using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ruse2023.Data;
using Ruse2023.Models;
using Ruse2023.Services.AccountService;
using Ruse2023.Services.ApiService;
using Ruse2023.Services.StoreService;
using System.Diagnostics;

namespace Ruse2023.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreService storeService;
        private readonly IApiService apiService;
        private readonly IAccountService accountService;
        private readonly ApplicationDbContext context;
        public HomeController(IStoreService storeService,
                              IApiService apiService,
                              IAccountService accountService,
                              ApplicationDbContext context)
        {
            this.storeService = storeService;
            this.apiService = apiService;
            this.accountService = accountService;
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var ap = new ApplicationStatisticsModel()
            {
                Users = await context.Users.CountAsync(),
                CreditsRewarded = await context.Credits.SumAsync(x => x.Ammount) + await context.BoughtProducts.SumAsync(x => x.Product.Price),
                PlantedTrees = await context.TreePlantApplications.Where(x => x.Status == "Approved").CountAsync(),
                EcoShopping = await context.ShoppingApplications.Where(x => x.Status == "Approved").CountAsync(),
            };

            return View(new HomePageModel
            {
                Products = await apiService.GetTopProducts(),
                Users = await accountService.GetUsers(),
                Stats = ap
            });
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}