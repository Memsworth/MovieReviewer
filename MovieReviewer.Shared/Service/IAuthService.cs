using Ardalis.Result;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Dto.Input;

namespace MovieReviewer.Shared.Service
{
    public interface IAuthService
    {
        public Task<Result<string>> LoginAsync(LoginModel loginData);
    }
}
