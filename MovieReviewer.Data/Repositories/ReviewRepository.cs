using Microsoft.EntityFrameworkCore;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Domain.Interfaces;

namespace MovieReviewer.Data.Repositories;

public class ReviewRepository(ApplicationDbContext context) : IGenericRepository<Review>
{
    public async Task Add(Review entity)
    {
        await context.Reviews.AddAsync(entity);
        await context.SaveChangesAsync();
    }
    public IEnumerable<Review> GetAll()
    {
        return context.Reviews;
    }
    public async Task<Review?> GetById(int reviewId)
    {
        return await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
    }
    public async Task Update(Review entity)
    {
        await context.SaveChangesAsync();
    }

    public async Task Delete(Review entity)
    {
        await context.SaveChangesAsync();
    }

    public Task<bool> Exists(int id)
    {
        throw new NotImplementedException();
    }
}