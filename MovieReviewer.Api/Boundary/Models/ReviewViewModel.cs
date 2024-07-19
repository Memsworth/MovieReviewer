namespace MovieReviewer.Api.Boundary.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public required string ReviewContent { get; set; }
        public double ReviewScore { get; set; }
        public int MovieId { get; set; }
    }
}
