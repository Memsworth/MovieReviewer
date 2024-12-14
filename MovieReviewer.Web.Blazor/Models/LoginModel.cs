using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Web.Blazor.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
