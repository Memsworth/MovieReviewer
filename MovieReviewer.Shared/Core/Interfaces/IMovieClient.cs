using MovieReviewer.Shared.Core.DTO.Outputs;

namespace MovieReviewer.Shared.Core.Interfaces
{
    public interface IMovieClient
    {
        Task<ImdbMovieDTO?> GetMovieInfoFromExternalApiAsync(string imdbId);
    }
}
