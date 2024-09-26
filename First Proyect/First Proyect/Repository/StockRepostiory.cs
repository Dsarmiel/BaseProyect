using First_Proyect.Data;
using First_Proyect.Dtos.Stock;
using First_Proyect.Helpers;
using First_Proyect.Interfaces;
using First_Proyect.Models;
using Microsoft.EntityFrameworkCore;

namespace First_Proyect.Repository
{
    public class StockRepostiory : IStockRepostory
    {
        private readonly ApplicationDBContext _context;
        public StockRepostiory(ApplicationDBContext dbContext) 
        { 
            _context = dbContext;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null) 
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName)) 
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol)) 
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy)) 
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase)) 
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol.Equals(symbol));
        }

        public async Task<bool> StockExist(int id)
        {
           return await _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stock)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null) 
            {
                return null;
            }
            stockModel.Symbol = stock.Symbol;
            stockModel.CompanyName = stock.CompanyName;
            stockModel.Purchase = stock.Purchase;
            stockModel.LastDiv = stock.LastDiv;
            stockModel.Industry = stock.Industry;
            stockModel.MarketCap = stock.MarketCap;

            await _context.SaveChangesAsync();

            return stockModel;
        }
    }
}
