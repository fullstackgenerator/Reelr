using Reelr.Data;

public class TmdbService
{
    private readonly HttpClient _httpClient;
    private readonly string? _apiKey;

    public TmdbService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["TMDB:ApiKey"];
        _httpClient.BaseAddress = new Uri(config["TMDB:BaseUrl"]);
    }

    public async Task<List<Movie>> GetPopularMoviesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<TmdbResponse>(
            $"movie/popular?api_key={_apiKey}&language=en-US&page=1");
        
        return response?.Results.Select(m => new Movie
        {
            Id = m.Id,
            Title = m.Title,
            Overview = m.Overview,
            VoteAverageTmdb = m.Vote_Average,
            PosterPath = m.Poster_Path,
            ReleaseYear = m.Release_Date?.Split('-')[0]
        }).ToList() ?? new List<Movie>();
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
        public string Poster_Path { get; set; }
        public string Release_Date { get; set; }
    }
}