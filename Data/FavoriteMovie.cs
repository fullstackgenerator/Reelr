namespace Reelr.Data;

public class FavoriteMovie
{
    public string UserId { get; set; } = null!;
    public int MovieId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public Movie Movie { get; set; } = null!;

}