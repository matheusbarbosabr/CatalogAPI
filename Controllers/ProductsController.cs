using AutoMapper;
using CatalogAPI.DTOs;
using CatalogAPI.Models;
using CatalogAPI.Repositories.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet("skip/{skip:int}/take/{take:int}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            try
            {
                if (take > 25)
                {
                    return BadRequest("Sorry, it's not possible to retrieve more than 25 results at a time.");
                }

                var total = await _productRepository.CountItemsAsync();
                var products = await _productRepository.GetAsync(skip, take);

                if (products is null)
                {
                    return NotFound("Products not found.");
                }

                var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

                return Ok(new
                {
                    total,
                    skip,
                    take,
                    data = productsDto
                });
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
