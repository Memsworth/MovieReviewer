using Ardalis.Result;

namespace MovieReviewer.Api.Control.Services
{
    public interface IMovieClient
    {
        Task<Result<MovieInformation>> GetMovieInfo(string imdbId);
    }
}
