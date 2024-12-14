
using MovieReviewer.Shared.Domain.Enums;

namespace MovieReviewer.Shared.Domain.Entities
{
    public class ApplicationUser : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserRole> Roles { get; set; } = [];
        public virtual ICollection<Review> Reviews { get; set; } = [];
        public virtual ICollection<Comment> Comments { get; set; } = [];
    }
}
