using Reelr.Data;

public class TmdbService
{
    private readonly HttpClient _httpClient;
    private readonly string? _apiKey;

    public TmdbService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
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

            var movies = response.Results.Select(m => new Movie
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
            });

            allMovies.AddRange(movies);
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