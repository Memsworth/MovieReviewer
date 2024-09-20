using Microsoft.EntityFrameworkCore;
using MovieReviewer.Shared.Infrastructure;

namespace MovieReviewer.Api.Features.Review
{
    public class ReviewRepository(ApplicationDbContext context)
    {
        public async Task Create(Shared.Core.Models.Review review)
        {
            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
        }
        
        public async Task<Shared.Core.Models.Review?> GetById(int reviewId) 
            => await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
        
        public IQueryable<Shared.Core.Models.Review> GetAll()
        {
            return context.Reviews;
        }

        public async Task Update()
        {
            await context.SaveChangesAsync();
        }

        public async Task Delete()
        {
            await context.SaveChangesAsync();
        }
     
    }
}
