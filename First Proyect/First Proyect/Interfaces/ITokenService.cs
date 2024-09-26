using First_Proyect.Models;

namespace First_Proyect.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
