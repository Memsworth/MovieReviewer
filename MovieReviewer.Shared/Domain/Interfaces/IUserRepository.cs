using MovieReviewer.Shared.Domain.Entities;

namespace MovieReviewer.Shared.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        public Task<bool> ExistByEmail(string email);
        public Task<ApplicationUser?> GetUserByEmail(string email);
    }
}
