using Ruse2023.Models.Store;

namespace Ruse2023.Services.ApiService
{
    public interface IApiService
    {
        Task<List<StoreApiModel>> GetTopProducts();
    }
}
