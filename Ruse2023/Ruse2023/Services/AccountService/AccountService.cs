using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ruse2023.Data;
using Ruse2023.Models.Account;
using System.Security.Claims;

namespace Ruse2023.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly ApplicationDbContext context;
        public AccountService(IHttpContextAccessor httpContext,
                              ApplicationDbContext context)
        {
            this.httpContext = httpContext;
            this.context = context;
        }
        public string GetUserId()
        {
            return httpContext?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<List<UserDisplayModel>> GetUsers()
        {
            var credits = await context.Credits.
                OrderByDescending(x => x.Ammount)
                .Take(3).ToListAsync();

            var res = new List<UserDisplayModel>(); 

            foreach (var user in credits)
            {
                var u = await context.Users.FindAsync(user.UserId);

                res.Add(new UserDisplayModel
                {
                    BirthDate = u.BirthDate,
                    PhoneNumber = u.PhoneNumber,
                    Credits = user.Ammount,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                });
            }

            return res;
        }
    }
}
