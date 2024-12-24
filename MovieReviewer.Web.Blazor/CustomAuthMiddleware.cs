using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.JsonWebTokens;
using MovieReviewer.Web.Blazor.Services;
using System.Security.Claims;

namespace MovieReviewer.Web.Blazor
{
    public class CustomAuthMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            if(httpContext.Request.Path.StartsWithSegments("/cookie-login"))
            {
                var token = httpContext.Request.Query["token"].ToString();
                AuthService.WebLoginQueue.TryDequeue(out string? loginToken);

                if (loginToken is null)
                {
                    Console.WriteLine("loginToken is null");
                    httpContext.Response.Redirect("/");
                    return;
                }
                if (string.IsNullOrEmpty(token) ||
                    loginToken != token)
                {
                    Console.WriteLine("Can't read 1");
                    httpContext.Response.Redirect("/");
                    return;
                }

                var tokenHandler = new JsonWebTokenHandler();
                if (!tokenHandler.CanReadToken(token))
                {
                    Console.WriteLine("Can't read 2");
                    httpContext.Response.Redirect("/");
                    return;
                }

                var claims = tokenHandler.ReadJsonWebToken(token).Claims;
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                await httpContext.SignInAsync(
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
            if(httpContext.Request.Path.StartsWithSegments("/cookie-logout"))
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                httpContext.Response.Redirect("/");
                return;
            }
            await next(httpContext);
        }
    }

    public static class CustomAuthMiddlewareExtentions
    {
        public static IApplicationBuilder UseCustomAuthMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<CustomAuthMiddleware>();
    }
}
