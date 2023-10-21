using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ruse2023.Models.Moderator;
using Ruse2023.Services.AccountService;
using Ruse2023.Services.ModeratorService;

namespace Ruse2023.Controllers
{
    public class ModeratorController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IModeratorService moderatorService;
        public ModeratorController(IAccountService accountService,
                                   IModeratorService moderatorService)
        {
            this.accountService = accountService;
            this.moderatorService = moderatorService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Apply()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Apply(ApplicationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await moderatorService.ApplyPost(accountService.GetUserId(), model);

            if (!result)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await moderatorService.GetDetails(id));
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            var result = await moderatorService.Approve(id);

            if (!result) return RedirectToAction("Error", "Home");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Decline(int id)
        {
            return View(new ApplicationDeclineModel() { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Decline(ApplicationDeclineModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await moderatorService.Decline(model);

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> MyApplications()
        {
            return View(await moderatorService.GetMyApplications(accountService.GetUserId()));
        }
        [Authorize]
        public async Task<IActionResult> ApplicationDetails(int id)
        {
            return View(await moderatorService.GetDetails(id));
        }
    }
}
