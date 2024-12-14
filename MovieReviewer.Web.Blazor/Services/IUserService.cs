using Ardalis.Result;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Web.Blazor.Models;
using BC = BCrypt.Net.BCrypt;

namespace MovieReviewer.Web.Blazor.Services
{
    public interface IUserService
    {
        public Task<Result<ApplicationUser>> GetUserByEmail(string email);
        public Task<bool> UserExists(string email);
        public bool VerifyPassword(string password, string storedPassword);
        public Task<Result> RegisterUser(RegisterModel entity);
    }
}
