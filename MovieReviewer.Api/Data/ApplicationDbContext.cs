using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Entities;

namespace MovieReviewer.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApiUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public required string DbPath { get; set; }
        public ApplicationDbContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var dbPath = Environment.GetFolderPath(folder);
            DbPath = Path.Join(dbPath, "movie.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
