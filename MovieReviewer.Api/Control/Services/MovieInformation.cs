namespace MovieReviewer.Api.Control.Services
{
    public class MovieInformation
    {
        public required string Title { get; set; }
        public required string Rated { get; set; }
        public required string Plot { get; set; }
        public required string Language { get; set; }
        public required string ImDbRating { get; set; }
        public required string ImDbId { get; set; }
    }
}
