using Microsoft.EntityFrameworkCore;
using MovieReviewer.Core.Domain;

namespace MovieReviewer.Core.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
        var folder = Environment.SpecialFolder.MyDocuments;
        var dbPath = Environment.GetFolderPath(folder);
        DbPath = Path.Join(dbPath, "movie.db");
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public required string DbPath { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}