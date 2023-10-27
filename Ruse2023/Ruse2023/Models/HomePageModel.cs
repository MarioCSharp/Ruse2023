using Ruse2023.Models.Account;
using Ruse2023.Models.Store;
using Ruse2023.Models.TreePlant;

namespace Ruse2023.Models
{
    public class HomePageModel
    {
        public List<StoreApiModel> Products { get; set; }
        public List<UserDisplayModel> Users { get; set; }
        public TreePlantApplicationModel TreePlant { get; set; }
    }
}
