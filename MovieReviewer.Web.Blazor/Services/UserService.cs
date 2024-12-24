using Ardalis.Result;
using MovieReviewer.Data.Repositories;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Domain.Enums;
using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Shared.Dto.Input;
using MovieReviewer.Shared.Service;
using BC = BCrypt.Net.BCrypt;

namespace MovieReviewer.Web.Blazor.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<Result> RegisterUser(RegisterModel entity)
        {
            var userExists = await userRepository.ExistByEmail(entity.Email);
            if (userExists)
                return Result.Error();

            var userInfo = new ApplicationUser()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Password = BC.HashPassword(entity.Password),
                Email = entity.Email,
                Roles = [UserRole.User]
            };

            await userRepository.Add(userInfo);
            return Result.Success();
        }

        public async Task<Result<ApplicationUser>> GetUserByEmail(string email)
        {
            var user = await userRepository.GetUserByEmail(email);
            return user is null ? Result.Error() : Result.Success(user);
        }
    }
}
