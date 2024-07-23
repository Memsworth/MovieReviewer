using Microsoft.AspNetCore.Identity;
using MovieReviewer.Api.Data;
using MovieReviewer.Api.Entities;

namespace MovieReviewer.Api.Control.Repository
{
    public class AuthR(ApplicationDbContext context, SignInManager<ApiUser> signInManager,
        RoleManager<ApiUser> roleManager, UserManager<ApiUser> userManager)
    {
        
    }
}
