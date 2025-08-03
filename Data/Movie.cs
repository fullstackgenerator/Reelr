using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reelr.Data;

public class Movie
{
    [Key]
    public int Id { get; set; }
    
    public int TmdbId { get; set; }
    
    public string? Title { get; set; }

    [Display(Name = "Release Year")]
    public string? ReleaseYear { get; set; }
    
    public string? GenresString { get; set; }
    
    [NotMapped]
    public List<string>? Genres 
    { 
        get => string.IsNullOrEmpty(GenresString) 
            ? new List<string>() 
            : GenresString.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        set => GenresString = value != null ? string.Join(",", value) : null;
    }

    public string? Overview { get; set; }
    
    [Display(Name = "TMDB Rating")]
    public float? VoteAverageTmdb { get; set; }

    public string? PosterPath { get; set; }
    
    public ICollection<FavoriteMovie> FavoriteMovies { get; set; } = new List<FavoriteMovie>();
}