﻿using Ruse2023.Models.Account;

namespace Ruse2023.Models.TreePlant
{
    public class TreePlantApprovalModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Credits { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public UserDisplayModel User { get; set; }
    }
}
