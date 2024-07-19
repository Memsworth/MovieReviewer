using MovieReviewer.Api.Entities.Utilities;

namespace MovieReviewer.Api.Boundary.Models
{
    public class MovieUpdateModel
    {
        public required string Title { get; set; }
        public required string Plot { get; set; }
        public RatingSystem MovieRating { get; set; }
        public Language MovieLanguage { get; set; }
        public required double ImdbRating { get; set; }
    }
}
