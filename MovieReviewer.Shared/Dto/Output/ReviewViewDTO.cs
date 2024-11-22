namespace MovieReviewer.Shared.Dto.Outputs;

public class ReviewViewDTO
{
    public int Id { get; set; }
    public required string ReviewContent { get; set; }
    public double ReviewScore { get; set; }
    public int MovieId { get; set; }
}