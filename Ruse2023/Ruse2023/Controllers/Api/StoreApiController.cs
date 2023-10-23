using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ruse2023.Data;
using Ruse2023.Data.Models;
using Ruse2023.Models.Store;
using Ruse2023.Services.AccountService;

namespace Ruse2023.Controllers.Api
{
    [ApiController]
    public class StoreApiController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAccountService accountService;
        public StoreApiController(ApplicationDbContext context,
                                  IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }
        [HttpGet]
        [Route("api/statistics")]
        public async Task<List<StoreApiModel>> GetTopProducts()
        {
            return await context.Products.Select(x => new StoreApiModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(x.Image))
            }).Take(3).ToListAsync();
        }
        [HttpGet]
        [Route("api/getCredits")]
        public async Task<StoreIntModel> GetUserCredits()
        {
            var credits = await context.Credits.FirstOrDefaultAsync(x => x.UserId == accountService.GetUserId());

            if (credits == null)
            {
                await context.Credits.AddAsync(new Credits
                {
                    UserId = accountService.GetUserId(),
                    Ammount = 0
                });
                await context.SaveChangesAsync();

                return new StoreIntModel() { Ammount = 0 };
            }

            return new StoreIntModel() { Ammount = credits.Ammount };
        }
    }
}
