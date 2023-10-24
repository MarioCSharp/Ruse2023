using Ruse2023.Models.Account;

namespace Ruse2023.Services.AccountService
{
    public interface IAccountService
    {
        string GetUserId();
        Task<List<UserDisplayModel>> GetUsers();
    }
}
