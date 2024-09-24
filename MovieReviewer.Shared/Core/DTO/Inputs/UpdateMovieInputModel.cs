using MovieReviewer.Shared.Core.Enums;

namespace MovieReviewer.Shared.Core.DTO.Inputs;

public class UpdateMovieInputModel
{
    public string Title { get; set; } = null!;
    public string Plot { get; set; } = null!;
    public RatingSystem MovieRating { get; set; }
    public Language MovieLanguage { get; set; }
    public double ImdbRating { get; set; }
    public bool Disabled { get; set; }
}