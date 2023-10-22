using Microsoft.AspNetCore.Mvc;
using Ruse2023.Models;
using Ruse2023.Services.StoreService;
using System.Diagnostics;

namespace Ruse2023.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreService storeService;
        public HomeController(IStoreService storeService)
        {
            this.storeService = storeService;
        }
        public async Task<IActionResult> Index()
        {
            await storeService.Initialize();
            return View();
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