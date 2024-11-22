namespace MovieReviewer.Shared.Domain.Entities;

public class Review : BaseEntity
{
    public int ReviewScore { get; set; }
    public int MovieId { get; set; }
    public required string Content { get; set; }
    public bool IsDeleted { get; set; } = false;
}