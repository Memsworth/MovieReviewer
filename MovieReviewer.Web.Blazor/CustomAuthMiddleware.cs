using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using MovieReviewer.Web.Blazor.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MovieReviewer.Web.Blazor
{
    public class CustomAuthMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            if(httpContext.Request.Path.StartsWithSegments("/cookie-login"))
            {
                var token = httpContext.Request.Query["token"];

                if (!string.IsNullOrEmpty(token) || 
                    !AuthService.WebLoginQueue.TryDequeue(out string? webLoginToken) ||
                    webLoginToken != token)
                {
                    httpContext.Response.Redirect("/");
                    return;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                if (!tokenHandler.CanReadToken(token))
                {
                    httpContext.Response.Redirect("/");
                    return;
                }

                var claims = tokenHandler.ReadJwtToken(token).Claims;
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
                await httpContext.SignInAsync(scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                    principal: claimsPrincipal,
                    properties: new AuthenticationProperties
                    {
                        IsPersistent = false,
                        IssuedUtc = DateTime.UtcNow,
                        AllowRefresh = false,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    });

                httpContext.Response.Redirect("/");
                return;
            }
            else if(httpContext.Request.Path.StartsWithSegments("/cookie-logout"))
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                httpContext.Response.Redirect("/");
                return;
            }
            else
            {
                await next(httpContext);
            }
        }
    }

    public static class CustomAuthMiddlewareExtentions
    {
        public static IApplicationBuilder UseCustomAuthMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<CustomAuthMiddleware>();
    }
}
