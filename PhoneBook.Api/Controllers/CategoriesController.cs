using Microsoft.AspNetCore.Mvc;
using PhoneBook.Api.Extensions;
using PhoneBook.Api.Repositories.Contracts;
using PhoneBook.Models.Dtos;

namespace PhoneBook.Api.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriesDto>>> GetCategories()
        {
            try
            {
                var categories = await this.categoriesRepository.GetCategories();

                if (categories == null)
                {
                    return NotFound();
                }
                else
                {
                    var categoryDtos = categories.ConvertToDto();
                    return Ok(categoryDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriesDto>> GetCategory(int id)
        {
            try
            {
                var category = await this.categoriesRepository.GetCategory(id);

                if (category == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(category);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}