using Cotizaciones.DBA;
using Cotizaciones.DBA.Models;
using Cotizaciones.Interfaces.Repositories;
using Cotizaciones.Utils.Filters;
using Microsoft.EntityFrameworkCore;

namespace Cotizaciones.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (categoryToDelete != null) 
            {
                _context.Categories.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
                return categoryToDelete;
            }
            return null;
        }

        public async Task<List<Category>> GetAll(QueryFilters queryFilters)
        {
            var categories = _context.Categories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryFilters.SortBy))
            {
                if (queryFilters.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
                {
                    categories = queryFilters.IsDecsending ? categories.OrderByDescending(c => c.Name) : categories.OrderBy(c => c.Name);
                }
            }
            
            var pagination = (queryFilters.PageNumber - 1) * queryFilters.PageSize;

            return await categories.Skip(pagination).Take(queryFilters.PageSize).ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(Guid id, Category category)
        {
            var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
                
                await _context.SaveChangesAsync();
                return categoryToUpdate;
            }
            return null;
        }
    }
}
