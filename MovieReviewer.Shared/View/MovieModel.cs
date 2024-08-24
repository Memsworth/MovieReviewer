using MovieReviewer.Shared.Core.Utilities;

namespace MovieReviewer.Shared.View
{
    public class MovieUpdateModel
    {
        public required string Title { get; set; }
        public required string Plot { get; set; }
        public RatingSystem MovieRating { get; set; }
        public Language MovieLanguage { get; set; }
        public required double ImdbRating { get; set; }
    }
    
    public class MovieViewModel : MovieUpdateModel
    {
        public int Id { get; set; }
        public required string ImdbId { get; set; }
    }
}
