using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook_API.Data;
using PhoneBook_API.Models;
using PhoneBook_API.Models.Dto;

namespace PhoneBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PhoneBookApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Get all contacts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ContactDTO>> GetContacts()
        {
            return Ok(_db.Contacts.ToList());
        }

        //Get single contact
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
            var contact = _db.Contacts.FirstOrDefault(x => x.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        //Create contact
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDTO> CreateContact([FromBody] ContactDTO contactDTO)
        {
            if (_db.Contacts.FirstOrDefault(u => u.Email.ToLower() == contactDTO.Email.ToLower()) != null)
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

            Contact model = new()
            {
                Id = contactDTO.Id,
                FirstName = contactDTO.FirstName,
                LastName = contactDTO.LastName,
                Email = contactDTO.Email,
                Password = contactDTO.Password,
                PhoneNumber = contactDTO.PhoneNumber,
                BirthDate = contactDTO.BirthDate
            };
            _db.Contacts.Add(model);
            _db.SaveChanges();

            return Ok(contactDTO);
        }

        //Delete contact
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
            var contact = _db.Contacts.FirstOrDefault(u => u.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            _db.Contacts.Remove(contact);
            _db.SaveChanges();

            return NoContent();
        }

        //Update all fields in specified contact
        [HttpPut("{id:int}", Name = "UpdateContact")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateContact(int id, [FromBody] ContactDTO contactDTO)
        {
            if (contactDTO == null || id != contactDTO.Id)
            {
                return BadRequest();
            }

            Contact model = new()
            {
                Id = contactDTO.Id,
                FirstName = contactDTO.FirstName,
                LastName = contactDTO.LastName,
                Email = contactDTO.Email,
                Password = contactDTO.Password,
                PhoneNumber = contactDTO.PhoneNumber,
                BirthDate = contactDTO.BirthDate
            };
            _db.Contacts.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

        //Update only choosen fields in specified contact
        [HttpPatch("{id:int}", Name = "UpdatePartialContact")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialContact(int id, JsonPatchDocument<ContactDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var contact = _db.Contacts.AsNoTracking().FirstOrDefault(u => u.Id == id);
             
            ContactDTO contactDTO = new()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Password = contact.Password,
                PhoneNumber = contact.PhoneNumber,
                BirthDate = contact.BirthDate
            };

            if (contact == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(contactDTO, ModelState);
            Contact model = new()
            {
                Id = contactDTO.Id,
                FirstName = contactDTO.FirstName,
                LastName = contactDTO.LastName,
                Email = contactDTO.Email,
                Password = contactDTO.Password,
                PhoneNumber = contactDTO.PhoneNumber,
                BirthDate = contactDTO.BirthDate
            };

            _db.Contacts.Update(model);
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
