using Microsoft.AspNetCore.Mvc;
using PhoneBook.Api.Entities;
using PhoneBook.Api.Repositories.Contracts;

namespace PhoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IContactRepository contactRepository;

        public CategoriesController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                var categories = await this.contactRepository.GetCategories();

                if(categories == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(categories);
                }
            }
            catch(Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var category = await this.contactRepository.GetCategory(id);

                if(category == null)
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
