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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        [HttpGet("products/skip/{skip:int}/take/{take:int}")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            try
            {
                if (take > 25)
                {
                    return BadRequest("Sorry, it's not possible to retrieve more than 25 results at a time.");
                }

                var total = await _categoryRepository.CountCategoriesProductsAsync();
                var categories = await _categoryRepository.GetCategoriesProductsAsync(skip, take);

                if (categories is null)
                {
                    return NotFound("Categories not foud.");
                }

                var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

                return Ok(new
                {
                    total,
                    skip,
                    take,
                    data = categoriesDto
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("skip/{skip:int}/take/{take:int}")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get([FromRoute] int skip = 0, [FromRoute] int take = 25)
        {
            try
            {
                if (take > 25)
                {
                    return BadRequest("Sorry, it's not possible to retrieve more than 25 results at a time.");
                }

                var total = await _categoryRepository.CountItemsAsync();
                var categories = await _categoryRepository.GetAsync(skip, take);

                if (categories is null)
                {
                    return NotFound("Categories not found");
                }

                var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

                return Ok(new
                {
                    total,
                    skip,
                    take,
                    data = categoriesDto
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(c => c.CategoryId == id);
                if (category is null)
                {
                    return NotFound("Category not found");
                }

                var categoryDto = _mapper.Map<CategoryDTO>(category);
                return Ok(categoryDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(CategoryDTO categoryDto)
        {
            try
            {
                if (categoryDto is null)
                {
                    return BadRequest();
                }

                var category = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.AddAsync(category);

                var categoryDTO = _mapper.Map<CategoryDTO>(category);

                return new CreatedAtRouteResult("GetCategory",
                    new { id = category.CategoryId }, categoryDTO);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CategoryDTO categoryDto)
        {
            try
            {
                if (id != categoryDto.CategoryId)
                {
                    return BadRequest();
                }

                var category = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.UpdateAsync(category);

                return Ok(categoryDto);
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

                return Ok("Category deleted successfully.");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
