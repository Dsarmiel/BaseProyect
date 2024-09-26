using Microsoft.AspNetCore.Identity;

namespace First_Proyect.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Portafolio> Portafolios { get; set; } = new List<Portafolio>();
    }
}
