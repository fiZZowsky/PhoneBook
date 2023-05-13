using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Api.Extensions;
using PhoneBook.Api.Repositories.Contracts;
using PhoneBook.Models.Dtos;

namespace PhoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetContacts()
        {
            try
            {
                var contacts = await this.contactRepository.GetContacts();
                var categories = await this.contactRepository.GetCategories();
                var subcategories = await this.contactRepository.GetSubcategories();

                if (contacts == null || categories == null)
                {
                    return NotFound();
                }
                else
                {
                    var contactDtos = contacts.ConvertToDto(categories, subcategories);

                    return Ok(contactDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContactDto>> GetContact(int id)
        {
            try
            {
                var contact = await this.contactRepository.GetContact(id);

                if (contact == null)
                {
                    return BadRequest();
                }
                else
                {
                    var category = await this.contactRepository.GetCategory(contact.CategoryId);
                    var subcategory = await this.contactRepository.GetSubcategory(contact.SubcategoryId ?? 0);
                    var contactDto = contact.ConvertToDto(category, subcategory);
                    return Ok(contactDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ContactDto>> CreateContact([FromBody] ContactDto contactDto)
        {
            try
            {
                var newContact = await this.contactRepository.AddContact(contactDto);
                if(newContact == null)
                {
                    return BadRequest(ModelState);
                }
                return Ok(newContact);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "NotUniqueEmailError. Email already exists!");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ContactDto>> DeleteContact(int Id)
        {
            try
            {
                var isDeleted = await this.contactRepository.DeleteContact(Id);

                if (isDeleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}