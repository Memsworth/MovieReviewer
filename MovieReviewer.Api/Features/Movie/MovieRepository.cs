using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Boundary;
using MovieReviewer.Api.Utilities;
using MovieReviewer.Shared.Infrastructure;
using MovieReviewer.Shared.View;

namespace MovieReviewer.Api.Features.Movie
{
    public class MovieRepository(ApplicationDbContext context)
    {
        public async Task Create(Shared.Core.Models.Movie movie)
        {
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
        }

        public async Task<Shared.Core.Models.Movie?> GetById(int movieId) 
            => await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);

        public async Task<Shared.Core.Models.Movie?> GetByImdbId(string imdbId) 
            => await context.Movies.FirstOrDefaultAsync(x => x.ImdbId == imdbId);

        public IQueryable<Shared.Core.Models.Movie> GetAll()
        {
            return context.Movies;
        }

        public async Task Update(Shared.Core.Models.Movie movie)
        {
            await context.SaveChangesAsync();
        }

        public async Task Delete()
        {
            await context.SaveChangesAsync();
        }
    }
}
