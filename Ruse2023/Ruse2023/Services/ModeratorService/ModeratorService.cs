using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ruse2023.Data;
using Ruse2023.Data.Models;
using Ruse2023.Models.Moderator;

namespace Ruse2023.Services.ModeratorService
{
    public class ModeratorService : IModeratorService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        public ModeratorService(ApplicationDbContext context,
                                UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<bool> ApplyPost(string userId, ApplicationModel model)
        {
            var application = new ModeratorApplication()
            {
                Description = model.Description,
                PhoneNumber = model.PhoneNumber,
                UserId = userId,
                Feedback = ""
            };

            await context.AddAsync(application);
            await context.SaveChangesAsync();

            return await context.ModeratorApplications.ContainsAsync(application);
        }

        public async Task<bool> Approve(int id)
        {
            var application = await context.ModeratorApplications.FindAsync(id);

            if (application == null) return false;

            var user = await context.Users.FindAsync(application.UserId);

            if (user == null) return false;

            await userManager.AddToRoleAsync(user, "Moderator");
            await context.SaveChangesAsync();

            application.Approved = true;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task Decline(ApplicationDeclineModel model)
        {
            var application = await context.ModeratorApplications.FindAsync(model.Id);

            if (application == null) return;

            application.Feedback = model.Feedback;
            application.Approved = false;
            await context.SaveChangesAsync();
        }

        public async Task<List<ApplicationApprovalModel>> GetAll()
        {
            var result = new List<ApplicationApprovalModel>();

            var loop = context.ModeratorApplications.Where(x => x.Approved == false && x.Feedback == "");

            foreach (var x in loop)
            {
                var user = await context.Users.FindAsync(x.UserId);

                result.Add(new ApplicationApprovalModel()
                {
                    Id = x.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Description = x.Description,
                    PhoneNumber = x.PhoneNumber,
                    UserId = x.UserId,
                    Approved = x.Approved,
                    Feedback = x.Feedback
                });
            }

            return result;
        }

        public async Task<ApplicationApprovalModel> GetDetails(int id)
        {
            var application = await context.ModeratorApplications.FindAsync(id);

            if (application == null) return null;

            var user = await context.Users.FindAsync(application.UserId);

            if (user == null) return null;

            return new ApplicationApprovalModel()
            {
                Id = application.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Description = application.Description,
                PhoneNumber = application.PhoneNumber,
                UserId = application.UserId,
                Approved = application.Approved,
                Feedback = application.Feedback
            };
        }
    }
}
