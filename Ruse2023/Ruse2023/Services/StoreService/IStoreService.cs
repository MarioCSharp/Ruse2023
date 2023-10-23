using Ruse2023.Models.Store;

namespace Ruse2023.Services.StoreService
{
    public interface IStoreService
    {
        Task<List<ProductModel>> GetAllProducts();
        Task Initialize();
        Task<bool> AddProduct(ProductModel model, List<IFormFile> Image);
        Task<ProductModel> ProductById(int id);
        Task<bool> BuyProduct(int id, string userId);
    }
}
