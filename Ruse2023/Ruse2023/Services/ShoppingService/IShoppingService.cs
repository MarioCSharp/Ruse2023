using Ruse2023.Models.Shopping;

namespace Ruse2023.Services.ShoppingService
{
    public interface IShoppingService
    {
        Task<bool> Create(ShoppingApplicationModel model, List<IFormFile> Image, string userId);
        Task<List<ShoppingApprovalModel>> GetAll();
        Task<ShoppingApprovalModel> GetDetails(int id);
        Task GiveCredits(ShoppingApprovalModel model);
        Task Decline(int id);
        Task<List<ShoppingApprovalModel>> GetMyApplications(string userId);
    }
}
