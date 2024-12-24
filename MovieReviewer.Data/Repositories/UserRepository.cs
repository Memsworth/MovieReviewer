using Microsoft.EntityFrameworkCore;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Domain.Interfaces;

namespace MovieReviewer.Data.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
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

        public async Task<bool> ExistByEmail(string email) =>
            await context.Users.AnyAsync(x => x.Email == email);

        public async Task<bool> Exists(int id) =>
            await context.Users.AnyAsync(x => x.Id == id);

        //TODO: do something here. Make it a query
        public IEnumerable<ApplicationUser> GetAll() => context.Users;

        public async Task<ApplicationUser?> GetById(int id) =>
             await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<ApplicationUser?> GetUserByEmail(string email) =>
            await context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task Update(ApplicationUser entity)
        {
            context.Users.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
