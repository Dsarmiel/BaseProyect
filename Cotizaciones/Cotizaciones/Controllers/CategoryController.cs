using Cotizaciones.Interfaces.Repositories;
using Cotizaciones.Utils.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Cotizaciones.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryFilters queryFilters) 
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var categories = await _categoryRepository.GetAll(queryFilters);
            if (categories == null) 
            {
                return NotFound();
            }
            return Ok(categories);
        }
    }
}
