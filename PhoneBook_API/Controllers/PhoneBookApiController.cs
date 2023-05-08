using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PhoneBook_API.Data;
using PhoneBook_API.Models.Dto;

namespace PhoneBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookApiControllerc : ControllerBase
    {
        public PhoneBookApiControllerc()
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ContactDTO>> GetContacts()
        {
            return Ok(PhoneBookStore.contactsList);
        }

        [HttpGet("{id:int}", Name = "GetContact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ContactDTO> GetContact(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var contact = PhoneBookStore.contactsList.FirstOrDefault(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDTO> CreateContact([FromBody] ContactDTO contactDTO)
        {
            if (PhoneBookStore.contactsList.FirstOrDefault(u => u.Email.ToLower() == contactDTO.Email.ToLower()) != null)
            {
                ModelState.AddModelError("NotUniqueEmailError", "Email already exists!");
                return BadRequest(ModelState);
            }

            if (contactDTO == null)
            {
                return BadRequest(contactDTO);
            }
            if (contactDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            contactDTO.Id = PhoneBookStore.contactsList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            PhoneBookStore.contactsList.Add(contactDTO);

            return Ok(contactDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteContact")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteContact(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var contact = PhoneBookStore.contactsList.FirstOrDefault(u => u.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            PhoneBookStore.contactsList.Remove(contact);

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateContact")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateContact(int id, [FromBody] ContactDTO contactDTO)
        {
            if (contactDTO == null || id != contactDTO.Id)
            {
                return BadRequest();
            }
            var contact = PhoneBookStore.contactsList.FirstOrDefault(u => u.Id == id);
            contact.FirstName = contactDTO.FirstName;
            contact.LastName = contactDTO.LastName;
            contact.Email = contactDTO.Email;
            contact.Password = contactDTO.Password;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialContact")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialContact(int id, JsonPatchDocument<ContactDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var contact = PhoneBookStore.contactsList.FirstOrDefault(u => u.Id == id);

            if (contact == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(contact, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
