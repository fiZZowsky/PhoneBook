using Microsoft.AspNetCore.Mvc;
using PhoneBook.Api.Entities;
using PhoneBook.Api.Repositories.Contracts;

namespace PhoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly IContactRepository contactRepository;

        public SubcategoriesController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subcategory>>> GetSubcategories()
        {
            try
            {
                var subcategories = await this.contactRepository.GetSubcategories();

                if (subcategories == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(subcategories);
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
                var subcategory = await this.contactRepository.GetSubcategory(id);

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
