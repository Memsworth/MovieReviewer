namespace MovieReviewer.Shared.Dto.Input;

public class UpdateReviewDto
{
    public required string ReviewContent { get; set; }
    public int ReviewScore { get; set; }
    public bool IsDisabled { get; set; }
}