using Ruse2023.Models.Moderator;

namespace Ruse2023.Services.ModeratorService
{
    public interface IModeratorService
    {
        Task<bool> ApplyPost(string userId, ApplicationModel model);
        Task<List<ApplicationApprovalModel>> GetAll();
        Task<ApplicationApprovalModel> GetDetails(int id);
        Task<bool> Approve(int id);
        Task Decline(ApplicationDeclineModel model);
    }
}
