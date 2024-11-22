using Microsoft.EntityFrameworkCore;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Domain.Interfaces;

namespace MovieReviewer.Data.Repositories;

public class MovieRepository(ApplicationDbContext context) : IMovieRepository
{
    public async Task<Movie?> GetByImdbId(string imdbId) => 
        await context.Movies.FirstOrDefaultAsync(x => x.ImdbId == imdbId);

    public async Task<Movie?> GetById(int id) =>
        await context.Movies.FirstOrDefaultAsync(x => x.Id == id);

    IEnumerable<Movie> IGenericRepository<Movie>.GetAll()
    {
        return GetAll();
    }

    public async Task Add(Movie entity)
    {
        await context.Movies.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(Movie entity)
    {
        context.Movies.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Movie entity)
    {
        context.Movies.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id) => 
        await context.Movies.AnyAsync(x => x.Id == id);

    public async Task<bool> ExistsByImdbId(string imdbId) => 
        await context.Movies.AnyAsync(m => m.ImdbId == imdbId);

    public IQueryable<Movie> GetAll()
    {
        return context.Movies;
    }
}