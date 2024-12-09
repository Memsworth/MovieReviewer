using Ardalis.Result;
using MovieReviewer.Shared.Dto.Input;
using MovieReviewer.Shared.Dto.Output;

namespace MovieReviewer.Shared.Service;

public interface IMovieService
{
    public Task<Result<MovieViewDto>> GetMovieAsync(int id);
    public Task<Result> CreateMovieAsync(CreateMovieDto entity);
    public IReadOnlyList<MovieViewDto> GetAllMoviesAsync();
    public Task<Result> UpdateMovieAsync(int id, UpdateMovieDto entity);
    public Task<Result> DeleteMovieAsync(int id);
    public Task<Result<CreateMovieDto>> GetMovieDataAsync(string imdbId);

}