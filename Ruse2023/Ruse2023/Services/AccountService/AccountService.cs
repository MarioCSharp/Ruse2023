using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Ruse2023.Data;
using System.Security.Claims;

namespace Ruse2023.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor httpContext;
        public AccountService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;

        }
        public string GetUserId()
        {
            return httpContext?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
