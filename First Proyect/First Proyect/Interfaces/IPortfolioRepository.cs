using First_Proyect.Models;

namespace First_Proyect.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortafolio(AppUser user);
        Task<Portafolio> CreateAsync(Portafolio portafolio);
        Task<Portafolio> DeleteAsync(AppUser user, string symbol);
    }
}
    