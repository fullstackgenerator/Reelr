using System.ComponentModel.DataAnnotations;

namespace Reelr.Data;

public class Movie
{
    [Key]
    public int Id { get; set; }
    
    public int TmdbId { get; set; }
    
    [Required]
    public string? Title { get; set; }

    [Display(Name = "Release Year")]
    public string? ReleaseYear { get; set; }

    public List<string>? Genres { get; set; }

    public string? Overview { get; set; }
    
    [Display(Name = "TMDB Rating")]
    public float? VoteAverageTmdb { get; set; }

    public string? PosterPath { get; set; }
    
    public ICollection<FavoriteMovie> FavoriteMovies { get; set; } = new List<FavoriteMovie>();
}