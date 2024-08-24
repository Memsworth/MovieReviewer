using MovieReviewer.Shared.Core.Utilities;

namespace MovieReviewer.Shared.Core.Models
{
    public class Comment : BaseEntity
    {
        public required string Content { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
