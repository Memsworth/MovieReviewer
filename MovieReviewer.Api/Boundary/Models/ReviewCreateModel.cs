namespace MovieReviewer.Api.Boundary.Models
{
    public class ReviewCreateModel
    {
        public required string ReviewContent { get; set; }
        public int ReviewScore { get; set; }
    }
}
