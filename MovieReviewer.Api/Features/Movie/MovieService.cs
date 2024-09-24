using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Shared.Core.DTO.Inputs;
using MovieReviewer.Shared.Core.DTO.Outputs;
using MovieReviewer.Shared.Core.Helpers;
using MovieReviewer.Shared.Core.Interfaces;

namespace MovieReviewer.Api.Features.Movie
{
    public class MovieService(MovieRepository movieRepository, IMovieClient movieClient)
    {
        public async Task<Result> CreateMovieAsync(CreateMovieInputModel createMovie)
        {
            //TODO: Check with Skod's cocurreny issue by addding in a Try catch block here
            var movie = await movieRepository.GetByImdbIdAsync(createMovie.ImdbId);

            if (movie is not null)
                return Result.Conflict();
            
            await movieRepository.CreateAsync(createMovie.ToMovieModel());
            return Result.Success();
        }
        
        public async Task<Result<CreateMovieInputModel>> CreateMovieInfoAsync(string imdbId)
        {
            var movieInfo = await movieClient.GetMovieInfoFromExternalApiAsync(imdbId);
            return movieInfo is null ? Result.NotFound() : Result.Success(movieInfo.ToCreateMovie());
        }

        public async Task<Result<MovieViewDTO>> GetMovieByIdAsync(int movieId)
        {
            var movie = await movieRepository.GetByIdAsync(movieId);

            if(movie is null || movie.IsDisabled)
                return Result.NotFound();

            return Result.Success(movie.ToMovieViewDTO());
        }

        public async Task<List<MovieViewDTO>> GetAllMoviesAsync()
        {
            var items = await movieRepository.GetAll().Where(x => x.IsDisabled != true)
                .Select(x => x.ToMovieViewDTO()).ToListAsync();
            return items;
        }

        public async Task<Result> UpdateMovieAsync(int movieId, UpdateMovieInputModel updateModel)
        {
            var movie = await movieRepository.GetByIdAsync(movieId);

            if (movie is null)
                return Result.NotFound();

            movie.Title = updateModel.Title;
            movie.Plot = updateModel.Plot;
            movie.MovieRating = updateModel.MovieRating;
            movie.MovieLanguage = updateModel.MovieLanguage;
            movie.ImdbRating = updateModel.ImdbRating;
            movie.IsDisabled = updateModel.Disabled;
            
            await movieRepository.UpdateAsync(movie);
            return Result.Success();
        }

        public async Task<Result> DeleteMovieAsync(int movieId)
        {
            var movie = await movieRepository.GetByIdAsync(movieId);

            if (movie is null)
                return Result.NotFound();

            if (movie.IsDisabled)
                return Result.Unavailable();

            movie.IsDisabled = true;
            movie.LastUpdatedAt = DateTime.UtcNow;

            await movieRepository.DeleteAsync();
            return Result.Success();
        }
    }
}
