/*using Ardalis.Result;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MovieReviewer.Api.Utilities;
using MovieReviewer.Core.Infrastructure;

namespace MovieReviewer.Api.Features.Auth;

public class JwtService(ApplicationDbContext context, IOptions<JwtConfig> jwtOptions)
{

    //TODO: There is a possibility that this might get an error. Put safeguards that return a Result.Error
    // First SafeGuard if the fields that are being used by apiUser are not null
    public string GenerateToken(ApiUser apiUser)
    {
        var key = Encoding.UTF8.GetBytes(jwtOptions.Value.SecurityKey);
        var securityKey = new SymmetricSecurityKey(key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, apiUser.Email),
                new Claim(ClaimTypes.Name, apiUser.UserName)
            }),

            Expires = DateTime.UtcNow.AddMinutes(60),
            IssuedAt = DateTime.UtcNow,
            Audience = jwtOptions.Value.Audience,
            Issuer = jwtOptions.Value.Issuer,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var generatedToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(generatedToken);
    }

    public async Task<Result> ValidateToken()
    {
        throw new NotImplementedException();
    }
}*/