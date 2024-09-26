using Cotizaciones.DBA.Models;
using Cotizaciones.Utils.Filters;

namespace Cotizaciones.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAll(ProductsFilters productsFilters);
        Task<Products?> GetById(Guid id);
        Task<Products> CreateAsync(Products category);
        Task<Products?> UpdateAsync(Guid id, Products category);
        Task<Products?> DeleteAsync(Guid id);
    }
}
