namespace MovieReviewer.Shared.Core.DTO.Inputs;

public class CreateReviewInputModel
{
    public required string ReviewContent { get; set; }
    public int ReviewScore { get; set; }
}