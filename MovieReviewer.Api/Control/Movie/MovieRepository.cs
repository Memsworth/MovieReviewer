using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Boundary;
using MovieReviewer.Api.Boundary.Models;
using MovieReviewer.Api.Control.Services;
using MovieReviewer.Api.Control.Utilities;
using MovieReviewer.Shared.Infrastructure;

namespace MovieReviewer.Api.Control.Movie
{
    public class MovieRepository(ApplicationDbContext context, IMovieClient movieClient)
    {
        public async Task<Result> CreateMovie(string imdbId)
        {
            //check if imdbid is in the db
            if (await context.Movies.FirstOrDefaultAsync(x => x.ImdbId == imdbId) is not null)
                return Result.Conflict();

            //call external api
            var responseFromClient = await movieClient.GetMovieInfo(imdbId);
            if (!responseFromClient.IsSuccess)
                return Result.NotFound();

            var item = responseFromClient.Value.ParseMovieData();
            await context.Movies.AddAsync(item);
            await context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<MovieViewModel>> GetMovieById(int movieId)
        {
            var item = await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            return item is null ? Result.NotFound() : Result.Success(item.ToMovieViewModel());
        }

        public async Task<Result<List<MovieViewModel>>> GetAllMovies()
        {
            var items = await context.Movies.Select(x => x.ToMovieViewModel()).ToListAsync();
            return Result.Success(items);
        }

        public async Task<Result> UpdateMovie(int movieId, MovieUpdateModel updateModel)
        {
            var item = await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            if (item is null)
                return Result.NotFound();
            item.Title = updateModel.Title;
            item.Plot = updateModel.Plot;
            item.MovieRating = updateModel.MovieRating;
            item.MovieLanguage = updateModel.MovieLanguage;
            item.ImdbRating = updateModel.ImdbRating;
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
