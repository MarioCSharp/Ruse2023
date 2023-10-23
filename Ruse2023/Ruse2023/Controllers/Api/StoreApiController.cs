using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ruse2023.Data;
using Ruse2023.Models.Store;

namespace Ruse2023.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StoreApiController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public StoreApiController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
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
    }
}
