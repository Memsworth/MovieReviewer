using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Shared.Dto.Output;

namespace MovieReviewer.Web.Blazor.Services;

public class MovieService (IMovieRepository repository)
{
    public IEnumerable<MovieViewDto> GetMovies()
    {
        return repository.GetAll().Select(x => new MovieViewDto()
        {
            Id = x.Id,
            Title = x.Title,
            Plot = x.Plot,
            ImdbRating = x.ImdbRating,
            MovieRating = x.MovieRating,
            MovieLanguage = x.MovieLanguage,
            ImdbId = x.ImdbId,
        }).ToList();
    }
}