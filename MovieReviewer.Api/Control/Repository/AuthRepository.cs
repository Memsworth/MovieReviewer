using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using MovieReviewer.Api.Boundary.Models;
using MovieReviewer.Api.Control.Services;
using MovieReviewer.Api.Data;
using MovieReviewer.Api.Entities;

namespace MovieReviewer.Api.Control.Repository
{
    public class AuthRepository
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly JwtService _jwtService;
        private readonly SignInManager<ApiUser> _signInManager;
        public AuthRepository(UserManager<ApiUser> userManager, JwtService jwtService, SignInManager<ApiUser> signInManager)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _signInManager = signInManager;
        }
        public async Task<Result<string>> RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            var newUser = new ApiUser();
            newUser.Email = userRegistrationModel.Email;
            newUser.UserName = userRegistrationModel.UserName;
            var result = await _userManager.CreateAsync(newUser, userRegistrationModel.Password);
            if (!result.Succeeded)
                return Result.Conflict();
            
            var token = _jwtService.GenerateToken(newUser);
            if (token is null)
                return Result.CriticalError();
            return Result.Success(token);
        }
        
        public async Task<Result<string>> LoginUser(UserLoginModel userLoginModel)
        {
            var user = await _userManager.FindByEmailAsync(userLoginModel.Email);
            if (user is null)
                return Result.NotFound();
            var result = await _userManager.CheckPasswordAsync(user, userLoginModel.Password);
            if (result is false)
                return Result.Invalid();

            var signInResult = await _signInManager.
                PasswordSignInAsync(userLoginModel.Email, userLoginModel.Password, false, false);
            if (!signInResult.Succeeded)
                return Result.CriticalError();

            var token = _jwtService.GenerateToken(user);
            if (token is null)
                return Result.Invalid();
            return Result.Success(token);
        }
    }
}
