/*using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Control.Repository;
using MovieReviewer.Domain.View;

namespace MovieReviewer.Api.Boundary
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(AuthRepository authRepository) : ControllerBase
    {
        private readonly RegisterValidator registerValidator = new();
        private readonly LoginValidator loginValidator = new();
        
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel registrationModel)
        {
            var validationResult = await registerValidator.ValidateAsync(registrationModel);
            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            var result = await authRepository.RegisterUser(registrationModel);
            if (!result.IsSuccess)
                return BadRequest("Error here");
            return Ok(new { result.Value });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel loginModel)
        {
            var validaitonResult = await loginValidator.ValidateAsync(loginModel);
            if (!validaitonResult.IsValid)
                return BadRequest(validaitonResult);

            var result = await authRepository.LoginUser(loginModel);
            if (!result.IsSuccess)
                return BadRequest();
            return Ok(new { result.Value });
        }
    }

    public class LoginValidator : AbstractValidator<UserLoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(5);
        }
    }

    public class RegisterValidator : AbstractValidator<UserRegistrationModel>
    {
        public RegisterValidator()
        {
            Include(new LoginValidator());
            RuleFor(x => x.UserName).MinimumLength(5).Matches("^[a-zA-Z0-9_.-]*$");
        }
    }
}*/