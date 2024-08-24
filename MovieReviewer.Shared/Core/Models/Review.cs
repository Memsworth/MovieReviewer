using MovieReviewer.Shared.Core.Utilities;

namespace MovieReviewer.Shared.Core.Models
{
    public class Review : Comment
    {
        public int ReviewScore { get; set; }
        public int MovieId { get; set; }
    }
}
