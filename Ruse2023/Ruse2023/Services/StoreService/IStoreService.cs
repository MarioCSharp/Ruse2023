using Ruse2023.Models.Store;

namespace Ruse2023.Services.StoreService
{
    public interface IStoreService
    {
        Task<List<ProductModel>> GetAllProducts();
        Task Initialize();
    }
}
