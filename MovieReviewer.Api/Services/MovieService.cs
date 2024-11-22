using Ardalis.Result;
using MovieReviewer.Api.Services.Helpers;
using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Shared.Dto.Input;
using MovieReviewer.Shared.Dto.Output;

namespace MovieReviewer.Api.Services
{
    public class MovieService(IMovieRepository movieRepository, IMovieClient movieClient)
    {
        public async Task<Result<CreateMovieDto>> GetMovieInfoFromApiAsync(string imdbId)
        {
            var movieInfo = await movieClient.GetMovieInfoFromExternalApiAsync(imdbId);
            return movieInfo is null ? Result.NotFound() : Result.Success(movieInfo.ToCreateMovieDto());
        }
        
        //TODO: Skod suggested concurrency issue. Solution: Try catch block

        public async Task<Result> CreateMovieAsync(CreateMovieDto movieDto)
        {
            var movie = await movieRepository.ExistsByImdbId(movieDto.ImdbId);

            if (movie)
                return Result.Conflict();
            
            await movieRepository.Add(movieDto.ToMovieModel());
            return Result.Success();
        }

        public async Task<Result<MovieViewDto>> GetMovieByIdAsync(int movieId)
        {
            var movie = await movieRepository.GetById(movieId);

            if(movie is null || movie.IsDisabled)
                return Result.NotFound();

            return Result.Success(movie.ToMovieViewDto());
        }

        public IReadOnlyList<MovieViewDto> GetAllMoviesAsync()
        {
            var items =  movieRepository.GetAll().Where(x => x.IsDisabled != true)
                .Select(x => x.ToMovieViewDto()).ToList();
            return items;
        }

        public async Task<Result> UpdateMovieAsync(int movieId, UpdateMovieDto movieDto)
        {
            var movie = await movieRepository.GetById(movieId);

            if (movie is null)
                return Result.NotFound();

            movie.Title = movieDto.Title;
            movie.Plot = movieDto.Plot;
            movie.MovieRating = movieDto.MovieRating;
            movie.MovieLanguage = movieDto.MovieLanguage;
            movie.ImdbRating = movieDto.ImdbRating;
            movie.IsDisabled = movieDto.Disabled;
            movie.LastUpdatedAt = DateTime.UtcNow;
            await movieRepository.Update(movie);
            return Result.Success();
        }

        public async Task<Result> DeleteMovieAsync(int movieId)
        {
            var movie = await movieRepository.GetById(movieId);

            if (movie is null)
                return Result.NotFound();

            if (movie.IsDisabled)
                return Result.Unavailable();

            movie.IsDisabled = true;
            movie.LastUpdatedAt = DateTime.UtcNow;

            await movieRepository.Update(movie);
            return Result.Success();
        }
    }
}
