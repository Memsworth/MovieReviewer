using MovieReviewer.Shared.Core.Enums;

namespace MovieReviewer.Shared.Core.DTO.Inputs;

public class MovieInputModel
{
    public string Title { get; set; }
    public string Plot { get; set; }
    public RatingSystem MovieRating { get; set; }
    public Language MovieLanguage { get; set; }
    public double ImdbRating { get; set; }
}