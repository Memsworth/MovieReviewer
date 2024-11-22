using MovieReviewer.Shared.Domain.Entities;

namespace MovieReviewer.Shared.Domain.Interfaces;

public interface IMovieRepository : IGenericRepository<Movie>
{
    //public Task<IEnumerable<Movie>> GetAllByTitle(string title);
    //public Task<Movie> GetByTitle(string title);
    //public Task<IEnumerable<Movie>> GetAllByRating(int rating);
    //public Task<Movie> GetByRating(int rating);
    //public Task<IEnumerable<Movie>> GetAllByGenre(string genre);
    public Task<bool> ExistsByImdbId(string imdbId);
    public Task<Movie?> GetByImdbId(string imdbId);
}