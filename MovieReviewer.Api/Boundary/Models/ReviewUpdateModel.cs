namespace MovieReviewer.Api.Boundary.Models
{
    public class ReviewUpdateModel : ReviewCreateModel
    {
        public bool IsDisabled { get; set; }
    }
}
