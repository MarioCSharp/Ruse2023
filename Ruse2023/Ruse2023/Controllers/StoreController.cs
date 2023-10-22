using Microsoft.AspNetCore.Mvc;
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
    }
}
