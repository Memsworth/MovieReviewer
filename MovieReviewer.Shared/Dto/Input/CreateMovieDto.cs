using MovieReviewer.Shared.Domain.Enums;

namespace MovieReviewer.Shared.Dto.Input;

public class CreateMovieDto
{
    public string ImdbId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Plot { get; set; } = string.Empty;
    public RatingSystem MovieRating { get; set; }
    public Language MovieLanguage { get; set; }
    public double ImdbRating { get; set; }
}