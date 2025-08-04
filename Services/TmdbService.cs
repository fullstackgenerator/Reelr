using Microsoft.EntityFrameworkCore;
using Reelr.Data;

public class TmdbService
{
    private readonly HttpClient _httpClient;
    private readonly string? _apiKey;
    private readonly ApplicationDbContext _context;

    public TmdbService(HttpClient httpClient, IConfiguration config, ApplicationDbContext dbContext)
    {
        _httpClient = httpClient;
        _context = dbContext;
        _apiKey = config["TMDB:ApiKey"];
        var baseUrl = config["TMDB:BaseUrl"];
        if (!string.IsNullOrEmpty(baseUrl))
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
        }
    }

    public async Task<List<Movie>> GetPopularMoviesAsync()
    {
        var genreMap = await GetGenreMapAsync();
        var allMovies = new List<Movie>();

        for (int page = 1; page <= 5; page++)
        {
            var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(
                $"movie/popular?api_key={_apiKey}&language=en-US&page={page}");

            if (response?.Results == null) continue;

            foreach (var m in response.Results)
            {
                var existingMovie = await _context.Movies
                    .FirstOrDefaultAsync(dbMovie => dbMovie.TmdbId == m.Id);

                if (existingMovie == null)
                {
                    var newMovie = new Movie
                    {
                        TmdbId = m.Id,
                        Title = m.Title,
                        Overview = m.Overview,
                        VoteAverageTmdb = m.Vote_Average,
                        PosterPath = m.Poster_Path,
                        ReleaseYear = m.Release_Date?.Split('-')[0],
                        GenresString = m.Genre_Ids != null && m.Genre_Ids.Any()
                            ? string.Join(",", m.Genre_Ids.Select(id => genreMap.GetValueOrDefault(id, "Unknown")))
                            : null
                    };

                    _context.Movies.Add(newMovie);
                    await _context.SaveChangesAsync();
                    allMovies.Add(newMovie);
                }
                else
                {
                    allMovies.Add(existingMovie);
                }
            }
        }
        return allMovies;
    }
    
    private async Task<Dictionary<int, string>> GetGenreMapAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<GenreResponse>(
                $"genre/movie/list?api_key={_apiKey}&language=en-US");

            return response?.Genres?.ToDictionary(g => g.Id, g => g.Name) ?? new Dictionary<int, string>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching genres: {ex.Message}");
            return new Dictionary<int, string>();
        }
    }

    private class TmdbResponse
    {
        public List<TmdbMovie>? Results { get; set; }
    }

    private class TmdbMovie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public float Vote_Average { get; set; }
        public string? Poster_Path { get; set; }
        public string? Release_Date { get; set; }
        public List<int>? Genre_Ids { get; set; }
    }

    private class GenreResponse
    {
        public List<TmdbGenre>? Genres { get; set; }
    }

    private class TmdbGenre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}