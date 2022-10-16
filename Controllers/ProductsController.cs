using CatalogAPI.Models;
using CatalogAPI.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            try
            {
                var products = await _productRepository.GetAsync();
                if (products is null)
                {
                    return NotFound("Products not found.");
                }
                return Ok(products);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(p => p.ProductId == id);
                if (product is null)
                {
                    return NotFound("Product not found.");
                }
                return Ok(product);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Product product)
        {
            try
            {
                if (product is null)
                {
                    return BadRequest();
                }

                await _productRepository.AddAsync(product);

                return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Product product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return BadRequest();
                }

                await _productRepository.UpdateAsync(product);

                return Ok(product);
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
                var product = await _productRepository.GetByIdAsync(p => p.ProductId == id);

                if (product is null)
                {
                    return NotFound("Product not found.");
                }

                await _productRepository.DeleteAsync(product);

                return NoContent();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
