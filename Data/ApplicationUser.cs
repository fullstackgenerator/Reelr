using Microsoft.AspNetCore.Identity;

namespace Reelr.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<FavoriteMovie> FavoriteMovies { get; set; } = new List<FavoriteMovie>();
    }

}
