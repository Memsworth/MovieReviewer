namespace MovieReviewer.Shared.View
{
    public class ReviewCreateModel
    {
        public required string ReviewContent { get; set; }
        public int ReviewScore { get; set; }
    }
    
    public class ReviewUpdateModel : ReviewCreateModel
    {
        public bool IsDisabled { get; set; }
    }
    
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public required string ReviewContent { get; set; }
        public double ReviewScore { get; set; }
        public int MovieId { get; set; }
    }
}
