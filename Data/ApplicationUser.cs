using Microsoft.AspNetCore.Identity;

namespace Reelr.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<FavoriteMovie> FavoriteMovie { get; set; } = new List<FavoriteMovie>();
    }

}
