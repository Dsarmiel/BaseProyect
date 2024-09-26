using First_Proyect.Dtos.Stock;
using First_Proyect.Models;

namespace First_Proyect.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel) 
        {
            return new StockDto 
            { 
                Id = stockModel.Id, 
                CompanyName = stockModel.CompanyName, 
                Industry = stockModel.Industry, 
                LastDiv = stockModel.LastDiv, 
                MarketCap = stockModel.MarketCap, 
                Purchase = stockModel.Purchase, 
                Symbol = stockModel.Symbol,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto createStockRequest)
        {
            return new Stock
            {
                Symbol = createStockRequest.Symbol,
                CompanyName = createStockRequest.CompanyName,
                Purchase = createStockRequest.Purchase,
                LastDiv = createStockRequest.LastDiv,
                Industry = createStockRequest.Industry,
                MarketCap = createStockRequest.MarketCap,
            };
        }
    }
}
