using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Boundary;
using MovieReviewer.Api.Features.Services;
using MovieReviewer.Api.Utilities;
using MovieReviewer.Shared.View;

namespace MovieReviewer.Api.Features.Movie
{
    public class MovieService(MovieRepository movieRepository, IMovieClient movieClient)
    {
        public async Task<Result> CreateMovie(string imdbId)
        {
            //TODO: Check with Skod's cocurreny issue by addding in a Try catch block here
            var movie = await movieRepository.GetByImdbId(imdbId);

            if (movie is not null)
                return Result.Conflict();

            //call external api to get moviedata
            var movieInfo = await movieClient.GetMovieInfo(imdbId);
            if (movieInfo is null)
                return Result.NotFound();
            
            var movieItem = movieInfo.ParseMovieData();
            await movieRepository.Create(movieItem);
            return Result.Success();
        }

        public async Task<Result<MovieViewModel>> GetMovieById(int movieId)
        {
            var movie = await movieRepository.GetById(movieId);

            if(movie is null || movie.IsDisabled)
                return Result.NotFound();

            return Result.Success(movie.ToMovieViewModel());
        }

        public async Task<List<MovieViewModel>> GetAllMovies()
        {
            var items = await movieRepository.GetAll().Where(x => x.IsDisabled != true)
                .Select(x => x.ToMovieViewModel()).ToListAsync();
            return items;
        }

        public async Task<Result> UpdateMovie(int movieId, MovieUpdateModel updateModel)
        {
            var movie = await movieRepository.GetById(movieId);

            if (movie is null)
                return Result.NotFound();

            movie.Title = updateModel.Title;
            movie.Plot = updateModel.Plot;
            movie.MovieRating = updateModel.MovieRating;
            movie.MovieLanguage = updateModel.MovieLanguage;
            movie.ImdbRating = updateModel.ImdbRating;

            await movieRepository.Update(movie);
            return Result.Success();
        }

        public async Task<Result> DeleteMovie(int movieId)
        {
            var movie = await movieRepository.GetById(movieId);

            if (movie is null)
                return Result.NotFound();

            movie.IsDisabled = true;
            movie.LastUpdatedAt = DateTime.UtcNow;

            await movieRepository.Delete();
            return Result.Success();
        }

    }
}
