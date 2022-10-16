using AutoMapper;
using CatalogAPI.DTOs;
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
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            try
            {
                var products = await _productRepository.GetAsync();
                if (products is null)
                {
                    return NotFound("Products not found.");
                }

                var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
                return Ok(productsDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(p => p.ProductId == id);
                if (product is null)
                {
                    return NotFound("Product not found.");
                }

                var productDto = _mapper.Map<ProductDTO>(product);
                return Ok(productDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductDTO productDto)
        {
            try
            {
                if (productDto is null)
                {
                    return BadRequest();
                }

                var product = _mapper.Map<Product>(productDto);
                await _productRepository.AddAsync(product);

                var productDTO = _mapper.Map<ProductDTO>(product);

                return new CreatedAtRouteResult("GetProduct", new { id = product.ProductId }, productDTO);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, ProductDTO productDto)
        {
            try
            {
                if (id != productDto.ProductId)
                {
                    return BadRequest();
                }

                var product = _mapper.Map<Product>(productDto);
                await _productRepository.UpdateAsync(product);

                return Ok(productDto);
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

                return Ok("Product deleted successfully.");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
