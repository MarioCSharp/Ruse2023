using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ruse2023.Services.ModeratorService;

namespace Ruse2023.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IModeratorService moderatorService;
        public AdministratorController(IModeratorService moderatorService)
        {
            this.moderatorService = moderatorService;
        }
        [HttpGet]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> Applications()
        {
            return View(await moderatorService.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> Approve(int applicationId) 
        {
            return View();
        }
    }
}
