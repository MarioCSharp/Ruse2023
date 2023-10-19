using Microsoft.EntityFrameworkCore;
using Ruse2023.Data;
using Ruse2023.Data.Models;
using Ruse2023.Models.TreePlant;
using System.Buffers.Text;

namespace Ruse2023.Services.TreePlantService
{
    public class TreePlantService : ITreePlantService
    {
        private readonly ApplicationDbContext context;
        public TreePlantService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> CreateApplication(TreePlantApplicationModel model, List<IFormFile> Image, string userId)
        {
            var application = new TreePlantApplication() { Description = model.Description,  UserId = userId};

            foreach (var file in Image)
            {
                if (file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        application.Image = stream.ToArray();
                    }
                }
            }

            await context.TreePlantApplications.AddAsync(application);
            await context.SaveChangesAsync();

            return await context.TreePlantApplications.ContainsAsync(application);
        }

        public async Task<List<TreePlantApprovalModel>> GetAll()
        {
            var result = new List<TreePlantApprovalModel>();

            foreach (var app in context.TreePlantApplications)
            {
                var base64 = Convert.ToBase64String(app.Image);
                var imgSrc = string.Format("data:image/gif;base64,{0}", base64);

                var model = new TreePlantApprovalModel()
                {
                    Id = app.Id,
                    Description = app.Description,
                    Image = imgSrc,
                    UserId = app.UserId
                };

                result.Add(model);
            }

            return result;
        }

        public async Task<TreePlantApprovalModel> GetDetails(int id)
        {
            var app = await context.TreePlantApplications.FindAsync(id);

            if (app == null) return null;

            return new TreePlantApprovalModel
            {
                Id = app.Id,
                Description = app.Description,
                Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(app.Image)),
                UserId = app.UserId
            };
        }

        public async Task GiveCredits(TreePlantApprovalModel model)
        {
            var credits = await context.Credits.FirstOrDefaultAsync(x => x.UserId == model.UserId);

            if (credits == null)
            {
                var cr = new Credits()
                {
                    UserId = model.UserId,
                    Ammount = model.Credits,
                };

                await context.Credits.AddAsync(cr);
                await context.SaveChangesAsync();

                return;
            }

            credits.Ammount += model.Credits;
            await context.SaveChangesAsync();
        }
    }
}
