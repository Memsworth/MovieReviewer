namespace MovieReviewer.Core.DTO.Inputs;

public class UpdateReviewInputModel
{
    public required string ReviewContent { get; set; }
    public int ReviewScore { get; set; }
    public bool IsDisabled { get; set; }
}