using First_Proyect.Dtos.Stock;
using First_Proyect.Helpers;
using First_Proyect.Models;

namespace First_Proyect.Interfaces
{
    public interface IStockRepostory
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stock);
        Task<Stock?> DeleteAsync(int id);
        Task<Boolean> StockExist(int id);
    }
}
