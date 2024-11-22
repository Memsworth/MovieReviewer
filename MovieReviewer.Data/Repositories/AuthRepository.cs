/*using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        protected ILookupNormalizer _normalizer;
        public AuthRepository(UserManager<ApiUser> userManager, JwtService jwtService,
            SignInManager<ApiUser> signInManager, ILookupNormalizer normalizer)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _signInManager = signInManager;
            _normalizer = normalizer;
        }
        public async Task<Result<string>> RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            var newUser = new ApiUser();
            newUser.Email = userRegistrationModel.Email;
            newUser.UserName = userRegistrationModel.UserName;
            newUser.NormalizedEmail = _normalizer.NormalizeEmail(newUser.Email);
            newUser.NormalizedUserName = _normalizer.NormalizeName(newUser.UserName);
            var result = await _userManager.CreateAsync(newUser, userRegistrationModel.Password);
            if (!result.Succeeded)
                return Result.Conflict();

            var token = _jwtService.GenerateToken(newUser);
            if (token is null)
                return Result.CriticalError();
            return Result.Success(token);
        }


        //TODO: Normalized email is not working correctly. Therefore, I am not using FindByEmailAsync
        //Come back and rewrite it to use FindByEmail
        public async Task<Result<string>> LoginUser(UserLoginModel userLoginModel)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == userLoginModel.Email);
            if (user is null)
                return Result.NotFound();
            var result = await _userManager.CheckPasswordAsync(user, userLoginModel.Password);
            if (result is false)
                return Result.Invalid();

            var signInResult = await _signInManager.
                PasswordSignInAsync(user.UserName, userLoginModel.Password, false, false);
            if (!signInResult.Succeeded)
                return Result.CriticalError();

            var token = _jwtService.GenerateToken(user);
            if (token is null)
                return Result.Invalid();
            return Result.Success(token);
        }
    }
}*/

