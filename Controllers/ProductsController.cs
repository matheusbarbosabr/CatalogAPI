using CatalogAPI.Data;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _context.Products.ToListAsync();
            if (products is null)
            {
                return NotFound("Products not found.");
            }
            return products;
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product is null)
            {
                return NotFound("Product not found.");
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Product product)
        {
            if (product is null)
                return BadRequest();

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetProduct",
                new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound("Product not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
