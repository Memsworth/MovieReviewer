namespace MovieReviewer.Core.DTO.Outputs;

public class ImdbMovieDTO
{
    public required string Title { get; set; }
    public required string Rated { get; set; }
    public required string Plot { get; set; }
    public required string Language { get; set; }
    public required string ImDbRating { get; set; }
    public required string ImDbId { get; set; }
}