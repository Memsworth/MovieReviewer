﻿using Microsoft.EntityFrameworkCore;
using MovieReviewer.Shared.Core.Domain;

namespace MovieReviewer.Shared.Infrastructure
{
    public class ApplicationDbContext : DbContext
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
