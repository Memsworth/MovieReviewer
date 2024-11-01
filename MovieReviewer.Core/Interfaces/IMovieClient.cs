using MovieReviewer.Core.DTO.Outputs;

namespace MovieReviewer.Core.Interfaces;

public interface IMovieClient
{
    Task<ImdbMovieDTO?> GetMovieInfoFromExternalApiAsync(string imdbId);
}