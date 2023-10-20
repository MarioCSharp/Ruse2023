using Ruse2023.Models.TreePlant;

namespace Ruse2023.Services.TreePlantService
{
    public interface ITreePlantService
    {
        Task<bool> CreateApplication(TreePlantApplicationModel model, List<IFormFile> Image, string userId);
        Task<List<TreePlantApprovalModel>> GetAll();
        Task<TreePlantApprovalModel> GetDetails(int id);
        Task GiveCredits(TreePlantApprovalModel model);
        Task Decline(int id);
    }
}
