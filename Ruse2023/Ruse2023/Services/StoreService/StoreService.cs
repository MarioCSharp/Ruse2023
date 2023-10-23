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

        public async Task<bool> AddProduct(ProductModel model, List<IFormFile> Image)
        {
            var m = new Product()
            {
                Name = model.Name,
                Price = model.Price,
            };

            foreach (var file in Image)
            {
                if (file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        m.Image = stream.ToArray();
                    }
                }
            }


            await context.Products.AddAsync(m);
            await context.SaveChangesAsync();

            return await context.Products.ContainsAsync(m);
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
