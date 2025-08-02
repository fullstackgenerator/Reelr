using Microsoft.EntityFrameworkCore;
using Reelr.Data;

namespace Reelr.Services;

public class MovieService
{
    private readonly ApplicationDbContext _context;
    private readonly TmdbService _tmdbService;

    public MovieService(ApplicationDbContext context, TmdbService tmdbService)
    {
        _context = context;
        _tmdbService = tmdbService;
    }
 
    public async Task<List<Movie>> GetMoviesAsync()
    {
        var existingMovies = await _context.Movies.ToListAsync();
        
        if (!existingMovies.Any())
        {
            var tmdbMovies = await _tmdbService.GetPopularMoviesAsync();
            
            _context.Movies.AddRange(tmdbMovies);
            await _context.SaveChangesAsync();
            return await _context.Movies.ToListAsync();
        }
    
        return existingMovies;
    }

    public async Task AddFavoriteAsync(string userId, int movieId)
    {
        try
        {
            if (await _context.FavoriteMovies.AnyAsync(f => f.UserId == userId && f.MovieId == movieId))
                return;
            
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);
            if (!movieExists)
            {
                Console.WriteLine($"Movie with ID {movieId} not found in database");
                return;
            }

            _context.FavoriteMovies.Add(new FavoriteMovie { UserId = userId, MovieId = movieId });
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred adding movie {movieId} to favorites: {ex.Message}");
            throw;
        }
    }

    public async Task RemoveFavoriteAsync(string userId, int movieId)
    {
        var favourite = await _context.FavoriteMovies
            .FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId);

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