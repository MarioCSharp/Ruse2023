using Microsoft.EntityFrameworkCore;
using Ruse2023.Controllers.Api;
using Ruse2023.Data;
using Ruse2023.Models.Store;

namespace Ruse2023.Services.ApiService
{
    public class ApiService : IApiService
    {
        private readonly ApplicationDbContext context;
        public ApiService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<StoreApiModel>> GetTopProducts()
        {
            return await context.Products.Select(x => new StoreApiModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(x.Image))
            }).OrderByDescending(x => x.Price).Take(3).ToListAsync();
        }
    }
}
