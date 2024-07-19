using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Entities;

namespace MovieReviewer.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public ApplicationDbContext()
        {
        }
    }
}
