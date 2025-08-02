using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Reelr.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<FavoriteMovie> FavoriteMovies { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasIndex(e => e.TmdbId).IsUnique();
        });
        
        builder.Entity<FavoriteMovie>()
            .HasKey(f => new { f.UserId, f.MovieId });
        
        builder.Entity<FavoriteMovie>()
            .HasOne(f => f.User)
            .WithMany(u => u.FavoriteMovies)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<FavoriteMovie>()
            .HasOne(f => f.Movie)
            .WithMany(m => m.FavoriteMovies)
            .HasForeignKey(f => f.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}