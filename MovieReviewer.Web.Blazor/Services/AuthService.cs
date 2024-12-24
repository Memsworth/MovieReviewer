using Ardalis.Result;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Shared.Dto.Input;
using MovieReviewer.Shared.Service;
using MovieReviewer.Web.Blazor.Models;
using System.Collections.Concurrent;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;


namespace MovieReviewer.Web.Blazor.Services
{
    public class AuthService(IOptions<JwtConfig> options, IUserRepository userRepository) : IAuthService
    {
        public static ConcurrentQueue<string> WebLoginQueue { get; set; } = new();

        public async Task<Result<string>> LoginAsync(LoginModel loginData)
        {
            var user = await userRepository.GetUserByEmail(loginData.Email);
            if (user is null)
                return Result.Error();

            var isPassword = BC.Verify(loginData.Password, user.Password);
            if (!isPassword)
                return Result.Error();

            var generatedToken = GenerateToken(user);

            WebLoginQueue.Enqueue(generatedToken);
            return Result.Success(generatedToken);
        }
        private string GenerateToken(ApplicationUser user)
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
            var signingCred = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var issuer = options.Value.Issuer;
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = options.Value.Issuer,
                Audience = options.Value.Audience,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCred,
                Subject = new ClaimsIdentity(claims)
            };
            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }
        
    }
}
