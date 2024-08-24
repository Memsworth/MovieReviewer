using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Boundary;
using MovieReviewer.Api.Features.Services;
using MovieReviewer.Api.Utilities;
using MovieReviewer.Shared.Infrastructure;
using MovieReviewer.Shared.View;

namespace MovieReviewer.Api.Features.Movie
{
    public class MovieRepository(ApplicationDbContext context, IMovieClient movieClient)
    {
        public async Task<Result> CreateMovie(string imdbId)
        {
            //1. Check if movie is in the db
            if (await context.Movies.FirstOrDefaultAsync(x => x.ImdbId == imdbId) is not null)
                return Result.Conflict("Movie Exists");

            //2. call external api to get moviedata
            var movieInfo = await movieClient.GetMovieInfo(imdbId);
            if (movieInfo is null)
                return Result.Error("Movie not found");

            var item = movieInfo.ParseMovieData();
            await context.Movies.AddAsync(item);
            await context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<MovieViewModel>> GetMovieById(int movieId)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            return movie is null ? Result.NotFound() : Result.Success(movie.ToMovieViewModel());
        }

        public async Task<Result<List<MovieViewModel>>> GetAllMovies()
        {
            var movies = await context.Movies.Select(x => x.ToMovieViewModel()).ToListAsync();
            return Result.Success(movies);
        }

        public async Task<Result> UpdateMovie(int movieId, MovieUpdateModel updateModel)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            if (movie is null)
                return Result.NotFound();
            movie.Title = updateModel.Title;
            movie.Plot = updateModel.Plot;
            movie.MovieRating = updateModel.MovieRating;
            movie.MovieLanguage = updateModel.MovieLanguage;
            movie.ImdbRating = updateModel.ImdbRating;
            await context.SaveChangesAsync();
            return Result.NoContent();
        }

        public async Task<Result> DeleteMovie(int movieId)
        {
            var item = await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            if (item is null)
                return Result.NotFound();

            item.IsDisabled = true;
            item.LastUpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return Result.NoContent();
        }
    }
}
