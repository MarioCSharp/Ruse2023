using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ruse2023.Data.Models;

namespace Ruse2023.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ModeratorApplication> ModeratorApplications { get; set; }
        public DbSet<Credits> Credits { get; set; }
        public DbSet<TreePlantApplication> TreePlantApplications { get; set; }
        public DbSet<ShoppingApplications> ShoppingApplications { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<BoughtProduct> BoughtProducts { get; set; }
    }
}