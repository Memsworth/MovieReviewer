using Ardalis.Result;
using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Shared.Dto.Input;
using MovieReviewer.Shared.Dto.Output;
using MovieReviewer.Shared.Service;

namespace MovieReviewer.Web.Blazor.Services;

public class MovieService (IMovieRepository repository, IMovieClient movieClient) : IMovieService
{ 
    public async Task<Result<CreateMovieDto>> GetMovieDataAsync(string imdbId) 
    { 
        var movieData = await movieClient.GetMovieInfoFromExternalApiAsync(imdbId); 
        return movieData is null ? Result.NotFound() : Result.Success(movieData.ToCreateMovieDto()); 
    }
    public async Task<Result<MovieViewDto>> GetMovieAsync(int movieId)
    {
        var movie = await repository.GetById(movieId);
        if(movie is null || movie.IsDisabled)
            return Result.NotFound();
        return Result.Success(movie.ToMovieViewDto());
    }
    public async Task<Result> CreateMovieAsync(CreateMovieDto entity) 
    {
        var movieExists = await repository.ExistsByImdbId(entity.ImdbId);
        if (movieExists)
            return Result.Conflict();
        await repository.Add(entity.ToMovieModel());
        return Result.Success();
    }
    public IReadOnlyList<MovieViewDto> GetAllMoviesAsync()
    {
        var items =  repository.GetAll()
            .Select(x => x.ToMovieViewDto()).ToList();
        return items;
    }
    public async Task<Result> UpdateMovieAsync(int id, UpdateMovieDto entity)
    {
        var movie = await repository.GetById(id);

        if (movie is null)
            return Result.NotFound();

        movie.Title = entity.Title;
        movie.Plot = entity.Plot;
        movie.MovieRating = entity.MovieRating;
        movie.MovieLanguage = entity.MovieLanguage;
        movie.ImdbRating = entity.ImdbRating;
        movie.IsDisabled = entity.Disabled;
        movie.LastUpdatedAt = DateTime.UtcNow;
        await repository.Update(movie);
        return Result.Success();
    }

    public async Task<Result> DeleteMovieAsync(int movieId)
    {
        var movie = await repository.GetById(movieId);
        if (movie is null)
            return Result.NotFound();
        if (movie.IsDisabled)
            return Result.Unavailable();

        movie.IsDisabled = true;
        movie.LastUpdatedAt = DateTime.UtcNow;
        await repository.Update(movie);
        return Result.Success();
    }
}