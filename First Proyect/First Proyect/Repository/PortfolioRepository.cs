using First_Proyect.Data;
using First_Proyect.Interfaces;
using First_Proyect.Models;
using Microsoft.EntityFrameworkCore;

namespace First_Proyect.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<Portafolio> CreateAsync(Portafolio portafolio)
        {
            await _context.Portafolios.AddAsync(portafolio);
            await _context.SaveChangesAsync();
            return portafolio;
        }

        public async Task<Portafolio> DeleteAsync(AppUser user, string symbol)
        {
            var portfolioModel = await _context.Portafolios.FirstOrDefaultAsync(x => x.AppUserId.Equals(user.Id) && x.Stock.Symbol.ToLower().Equals(symbol.ToLower()));
            if (portfolioModel == null) return null;
            _context.Portafolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortafolio(AppUser user)
        {
            return await _context.Portafolios.Where(u => u.AppUserId == user.Id).Select(stock => new Stock 
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap,
            }).ToListAsync();
        }

    }
}
