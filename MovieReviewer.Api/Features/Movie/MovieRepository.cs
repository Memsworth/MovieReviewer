using Microsoft.EntityFrameworkCore;
using MovieReviewer.Shared.Infrastructure;

namespace MovieReviewer.Api.Features.Movie
{
    public class MovieRepository(ApplicationDbContext context)
    {
        public async Task CreateAsync(Shared.Core.Domain.Movie movie)
        {
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
        }

        public async Task<bool> MovieExistsById(int id) => 
            await context.Movies.AnyAsync(m => m.Id == id);
        public async Task<bool> MovieExistByImdbId(string imdbId) => 
            await context.Movies.AnyAsync(m => m.ImdbId == imdbId);

        public async Task<Shared.Core.Domain.Movie?> GetByIdAsync(int movieId) 
            => await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);

        public async Task<Shared.Core.Domain.Movie?> GetByImdbIdAsync(string imdbId) 
            => await context.Movies.FirstOrDefaultAsync(x => x.ImdbId == imdbId);

        public IQueryable<Shared.Core.Domain.Movie> GetAll()
        {
            return context.Movies;
        }

        public async Task UpdateAsync(Shared.Core.Domain.Movie movie)
        {
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
