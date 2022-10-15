using CatalogAPI.Data;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesProducts()
        {
            try
            {
                var categories = await _context.Categories.Include(p => p.Products).AsNoTracking().ToListAsync();
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
                var categories = await _context.Categories.AsNoTracking().ToListAsync();
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
                var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
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

                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

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

                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();

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
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                if (category is null)
                {
                    return NotFound("Category not found");
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
