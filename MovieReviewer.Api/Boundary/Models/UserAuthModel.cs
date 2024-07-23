namespace MovieReviewer.Api.Boundary.Models;

public class UserLoginModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UserRegistrationModel : UserLoginModel
{
    public required string UserName { get; set; }
}