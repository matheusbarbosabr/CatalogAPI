using CatalogAPI.Models;
using CatalogAPI.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesProducts()
        {
            try
            {
                var categories = await _categoryRepository.GetCategoriesProductsAsync();
                if (categories is null)
                {
                    return NotFound("Categories not foud.");
                }
                return Ok(categories);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            try
            {
                var categories = await _categoryRepository.GetAsync();
                if (categories is null)
                {
                    return NotFound("Categories not found");
                }
                return Ok(categories);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(c => c.CategoryId == id);
                if (category is null)
                {
                    return NotFound("Category not found");
                }
                return Ok(category);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Category category)
        {
            try
            {
                if (category is null)
                    return BadRequest();

                await _categoryRepository.AddAsync(category);

                return new CreatedAtRouteResult("GetCategory",
                    new { id = category.CategoryId }, category);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return BadRequest();
                }

                await _categoryRepository.UpdateAsync(category);

                return Ok(category);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(c => c.CategoryId == id);
                if (category is null)
                {
                    return NotFound("Category not found");
                }

                await _categoryRepository.DeleteAsync(category);

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
