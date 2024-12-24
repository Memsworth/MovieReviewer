using Ardalis.Result;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Dto.Input;

namespace MovieReviewer.Shared.Service
{
    public interface IUserService
    {
        public Task<Result<ApplicationUser>> GetUserByEmail(string email);
        public Task<Result> RegisterUser(RegisterModel entity);
    }
}
