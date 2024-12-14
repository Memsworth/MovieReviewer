using MovieReviewer.Shared.Domain.Enums;

namespace MovieReviewer.Shared.Domain.Entities;

public class Movie : BaseEntity
{
    public required string Title { get; set; }
    public required string Plot { get; set; }
    public RatingSystem MovieRating { get; set; }
    public Language MovieLanguage { get; set; }
    public required string ImdbId { get; set; }
    public required double ImdbRating { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public bool IsDisabled { get; set; }
    public virtual ICollection<Review> Reviews { get; set; } = [];
}