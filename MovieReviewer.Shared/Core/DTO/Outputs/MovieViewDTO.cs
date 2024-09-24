using MovieReviewer.Shared.Core.Enums;

namespace MovieReviewer.Shared.Core.DTO.Outputs;

public class MovieViewDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Plot { get; set; } = null!;
    public RatingSystem MovieRating { get; set; }
    public Language MovieLanguage { get; set; }
    public string ImdbId { get; set; } = null!;
    public double ImdbRating { get; set; }
}