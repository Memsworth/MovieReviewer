using Microsoft.EntityFrameworkCore;
using MovieReviewer.Core.Domain;

namespace MovieReviewer.Core.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context)
{
    public async Task Create(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetById(int reviewId)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == reviewId);
    }

    public IQueryable<User> GetAll()
    {
        return context.Users;
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