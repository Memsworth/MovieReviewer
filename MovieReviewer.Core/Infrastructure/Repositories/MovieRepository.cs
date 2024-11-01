using Microsoft.EntityFrameworkCore;
using MovieReviewer.Core.Domain;

namespace MovieReviewer.Core.Infrastructure.Repositories;

public class MovieRepository(ApplicationDbContext context)
{
    public async Task CreateAsync(Movie movie)
    {
        await context.Movies.AddAsync(movie);
        await context.SaveChangesAsync();
    }

    public async Task<bool> MovieExistsById(int id)
    {
        return await context.Movies.AnyAsync(m => m.Id == id);
    }

    public async Task<bool> MovieExistByImdbId(string imdbId)
    {
        return await context.Movies.AnyAsync(m => m.ImdbId == imdbId);
    }

    public async Task<Movie?> GetByIdAsync(int movieId)
    {
        return await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
    }

    public async Task<Movie?> GetByImdbIdAsync(string imdbId)
    {
        return await context.Movies.FirstOrDefaultAsync(x => x.ImdbId == imdbId);
    }

    public IQueryable<Movie> GetAll()
    {
        return context.Movies;
    }

    public async Task UpdateAsync(Movie movie)
    {
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync()
    {
        await context.SaveChangesAsync();
    }
}