using Microsoft.EntityFrameworkCore;
using MovieReviewer.Shared.Domain.Entities;

namespace MovieReviewer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.MyDocuments;
        var dbPath = Environment.GetFolderPath(folder);
        DbPath = Path.Join(dbPath, "movie.db");
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public required string DbPath { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}