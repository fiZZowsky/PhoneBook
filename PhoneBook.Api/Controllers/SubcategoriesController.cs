using Microsoft.AspNetCore.Mvc;
using PhoneBook.Api.Entities;
using PhoneBook.Api.Extensions;
using PhoneBook.Api.Repositories.Contracts;

namespace PhoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : Controller
    {
        private readonly ISubcategoriesRepository subcategoriesRepository;

        public SubCategoriesController(ISubcategoriesRepository subcategoriesRepository)
        {
            this.subcategoriesRepository = subcategoriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subcategory>>> GetSubcategories()
        {
            try
            {
                var subcategories = await this.subcategoriesRepository.GetSubcategories();

                if (subcategories == null)
                {
                    return NotFound();
                }
                else
                {
                    var subcategoryDtos = subcategories.ConvertToDto();
                    return Ok(subcategoryDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Subcategory>> GetSubcategory(int id)
        {
            try
            {
                var subcategory = await this.subcategoriesRepository.GetSubcategory(id);

                if (subcategory == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(subcategory);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }
    }
}
