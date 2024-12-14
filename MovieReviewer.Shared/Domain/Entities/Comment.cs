namespace MovieReviewer.Shared.Domain.Entities;

public class Comment : BaseEntity
{
    public required string Content { get; set; }
    public bool IsDeleted { get; set; } = false;
    public int ReviewId { get; set; }
    public virtual Review Review { get; set; }
}