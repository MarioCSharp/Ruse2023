using Microsoft.EntityFrameworkCore;
using Ruse2023.Data;
using Ruse2023.Data.Models;
using Ruse2023.Models.Store;

namespace Ruse2023.Services.StoreService
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext context;
        public StoreService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            return await context.Products
                .Select(x => new ProductModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Price = x.Price

                }).ToListAsync();
        }

        public async Task Initialize()
        {
            await Console.Out.WriteLineAsync();
        }
    }
}
