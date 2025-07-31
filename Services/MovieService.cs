using Microsoft.EntityFrameworkCore;
using Reelr.Data;

namespace Reelr.Services;

public class MovieService
{
    private readonly ApplicationDbContext _context;

    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Movie>> GetMoviesAsync()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task AddMovieAsync(string userId, int movieId)
    {
        _context.FavoriteMovies.Add(new FavoriteMovie { UserId = userId, MovieId = movieId });
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFavoriteAsync(string userId, int movieId)
    {
        var favourite = _context.FavoriteMovies.FirstOrDefault(f => f.UserId == userId && f.MovieId == movieId);

        if (favourite != null)
        {
            _context.FavoriteMovies.Remove(favourite);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Movie>> GetUserFavoritesAsync(string userId)
    {
        return await _context.FavoriteMovies
            .Where(f => f.UserId == userId)
            .Include(f => f.Movie)
            .Select(f => f.Movie)
            .ToListAsync();
    }
}