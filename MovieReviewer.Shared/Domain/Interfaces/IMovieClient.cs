namespace MovieReviewer.Shared.Domain.Interfaces;

public interface IMovieClient
{
    Task<ImdbMovieDTO?> GetMovieInfoFromExternalApiAsync(string imdbId);
}