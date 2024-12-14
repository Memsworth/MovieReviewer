using Microsoft.EntityFrameworkCore;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Domain.Interfaces;

namespace MovieReviewer.Data.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IGenericRepository<ApplicationUser>
    {
        public async Task Add(ApplicationUser entity)
        {
            await context.Users.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(ApplicationUser entity)
        {
            context.Users.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await context.Users.AnyAsync(x => x.Id == id);
        }

        //TODO: do something here. Make it a query
        public IEnumerable<ApplicationUser> GetAll()
        {
            return context.Users;
        }

        public async Task<ApplicationUser?> GetById(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public async Task Update(ApplicationUser entity)
        {
            context.Users.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
