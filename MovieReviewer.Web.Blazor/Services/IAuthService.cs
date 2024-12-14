using Ardalis.Result;
using Microsoft.Extensions.Options;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Web.Blazor.Models;

namespace MovieReviewer.Web.Blazor.Services
{
    public interface IAuthService
    {
        public string GenerateToken(ApplicationUser user);
        public Task<Result<string>> LoginAsync(LoginModel loginData);
        public Task<Result> RegisterAsync(RegisterModel registerData);
    }
}
