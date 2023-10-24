using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ruse2023.Data;
using Ruse2023.Data.Models;
using Ruse2023.Models.Account;

namespace Ruse2023.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private readonly ApplicationDbContext context;
        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }

            var user = new User
            {
                Email = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserName = registerModel.Email,
                EmailConfirmed = true,
                PhoneNumber = registerModel.PhoneNumber,
                BirthDate = registerModel.BirthDate
            };

            var result = await userManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                await context.Credits.AddAsync(new Credits()
                {
                    UserId = user.Id,
                    Ammount = 0
                });
                await context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");

            return View(registerModel);
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var user = await userManager.FindByEmailAsync(loginModel.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, true, false);

                if (result.Succeeded)
                {
                    if (loginModel.ReturnUrl != null)
                    {
                        return Redirect(loginModel.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login!");

            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Manage()
        {
            return View();
        }
    }
}
