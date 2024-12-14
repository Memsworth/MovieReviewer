using Ardalis.Result;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Web.Blazor.Models;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieReviewer.Web.Blazor.Services
{
    public class AuthService(IOptions<JwtConfig> options, IUserService userService) : IAuthService
    {
        public static ConcurrentQueue<string> WebLoginQueue { get; set; } = new();

        public async Task<Result> RegisterAsync(RegisterModel registerData)
        {
            var userExists = await userService.UserExists(registerData.Email);
            if (userExists)
                return Result.Error();

            var result = await userService.RegisterUser(registerData);
            return result.IsSuccess ? Result.Success() : Result.Error();
        }
        public async Task<Result<string>> LoginAsync(LoginModel loginData)
        {
            var user = await userService.GetUserByEmail(loginData.Email);
            if (!user.IsSuccess)
                return Result.Error();

            var isPassword = userService.VerifyPassword(loginData.Password, user.Value.Password);
            if (!isPassword)
                return Result.Error();

            var generatedToken = GenerateToken(user.Value);

            WebLoginQueue.Enqueue(generatedToken);
            return Result.Success(generatedToken);
        }
        public string GenerateToken(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, $"{user.FirstName}.{user.LastName}"),
                new Claim(ClaimTypes.Email, $"{user.Email}"),
            }.Concat(user.Roles.Select(x => new Claim(ClaimTypes.Role, x.ToString())));

            return BuildToken(claims);
        }

        private string BuildToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
            var signingCred = new SigningCredentials(secretKey, SecurityAlgorithms.Sha256);
            var issuer = options.Value.Issuer;
            var tokenDescriptor = new JwtSecurityToken(
                issuer: options.Value.Issuer,
                audience: options.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCred
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        
    }
}
