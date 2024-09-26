using Cotizaciones.DBA.Models;
using Cotizaciones.Utils.Filters;

namespace Cotizaciones.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll(QueryFilters queryFilters);
        Task<Category?> GetById(Guid id);
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Guid id, Category category);
        Task<Category?> DeleteAsync(Guid id);
    }
}
