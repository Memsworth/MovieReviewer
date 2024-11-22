namespace MovieReviewer.Shared.Dto.Input;

public class CreateReviewDto
{
    public string Content { get; set; } = string.Empty;
    public int Score { get; set; }
}