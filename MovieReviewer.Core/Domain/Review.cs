namespace MovieReviewer.Core.Domain;

public class Review : Comment
{
    public int ReviewScore { get; set; }
    public int MovieId { get; set; }
}