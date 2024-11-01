using Microsoft.EntityFrameworkCore;
using MovieReviewer.Core.Domain;

namespace MovieReviewer.Core.Infrastructure.Repositories;

public class ReviewRepository(ApplicationDbContext context)
{
    public async Task Create(Review review)
    {
        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();
    }

    public async Task<Review?> GetById(int reviewId)
    {
        return await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
    }

    public IQueryable<Review> GetAll()
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