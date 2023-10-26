using Microsoft.EntityFrameworkCore;
using Ruse2023.Data;
using Ruse2023.Data.Models;
using Ruse2023.Models.Store;
using Ruse2023.Services.AccountService;
using System.Buffers.Text;

namespace Ruse2023.Services.StoreService
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext context;
        private readonly IAccountService accountService;
        public StoreService(ApplicationDbContext context,
                            IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
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

        public async Task<bool> BuyProduct(int id, string userId)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null) return false;

            var credits = await context.Credits.FirstOrDefaultAsync(x => x.UserId == userId);

            if (credits == null) return false;

            if (credits.Ammount < product.Price) return false;

            credits.Ammount -= product.Price;
            await context.SaveChangesAsync();

            var bp = new BoughtProduct()
            {
                ProductId = product.Id,
                UserId = userId
            };

            await context.BoughtProducts.AddAsync(bp);
            await context.SaveChangesAsync();

            return await context.BoughtProducts.ContainsAsync(bp);
        }

        public async Task<List<ProductModel>> GetAllProducts()
        {
            return await context.Products
                .Select(x => new ProductModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(x.Image)),
                    Price = x.Price
                }).ToListAsync();
        }

        public async Task Initialize()
        {
            await Console.Out.WriteLineAsync();
        }

        public async Task<ProductCheckOutModel> ProductById(int id)
        {
            var m = await context.Products.FindAsync(id);

            if (m == null) return null;

            var user = await context.Users.FindAsync(accountService.GetUserId());

            return new ProductCheckOutModel()
            {
                Id = id,
                Name = m.Name,
                Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(m.Image)),
                Price = m.Price,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}
