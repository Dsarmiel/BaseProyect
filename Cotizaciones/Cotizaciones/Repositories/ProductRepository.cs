using Cotizaciones.DBA.Models;
using Cotizaciones.Interfaces.Repositories;
using Cotizaciones.Utils.Filters;

namespace Cotizaciones.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task<Products> CreateAsync(Products category)
        {
            throw new NotImplementedException();
        }

        public Task<Products?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Products>> GetAll(ProductsFilters productsFilters)
        {
            throw new NotImplementedException();
        }

        public Task<Products?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Products?> UpdateAsync(Guid id, Products category)
        {
            throw new NotImplementedException();
        }
    }
}
