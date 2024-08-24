using Ardalis.Result;

namespace MovieReviewer.Api.Features.Services
{
    public interface IMovieClient
    {
        Task<MovieInformation?> GetMovieInfo(string imdbId);
    }
}
