﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PhoneBook.Server.Data;
using PhoneBook.Shared;

namespace PhoneBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetContacts()
        {
            var contacts = await _context.Contacts
                .Include(c => c.Category)
                .Include(s => s.Subcategory)
                .ToListAsync();
            return Ok(contacts);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("subcategories")]
        public async Task<ActionResult<List<Subcategory>>> GetSubcategories()
        {
            var subcategories = await _context.Subcategories.ToListAsync();
            return Ok(subcategories);
        }

        [HttpGet("usercategories")]
        public async Task<ActionResult<List<UserCategory>>> GetUserCategories()
        {
            var userCategories = await _context.UserCategories.ToListAsync();
            return Ok(userCategories);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _context.Contacts
                .Include(c => c.Category)
                .Include(s => s.Subcategory)
                .Include(u => u.UserCategory)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (contact == null)
                return NotFound("Contact not found. :/");

            return Ok(contact);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<List<Contact>>> CreateContact(Contact contact)
        {
            contact.Category = null;
            contact.Subcategory = null;
            contact.UserCategory = null;

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return Ok(await GetDbContacts());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<List<UserCategory>>> CreateNewCategory(UserCategory userCategory)
        {
            _context.UserCategories.Add(userCategory);
            await _context.SaveChangesAsync();

            return Ok(await GetUserCategories());
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Contact>>> UpdateContact(Contact contact, int id)
        {
            var dbContact = await _context.Contacts
                .Include(c => c.Category)
                .Include(s => s.Subcategory)
                .Include(u => u.UserCategory)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (dbContact == null)
            {
                return NotFound("Sorry, but no here contact for you. :/");
            }

            dbContact.FirstName = contact.FirstName;
            dbContact.LastName = contact.LastName;
            dbContact.Email = contact.Email;
            dbContact.Password = contact.Password;
            dbContact.PhoneNumber = contact.PhoneNumber;
            dbContact.BirthDate = contact.BirthDate;
            dbContact.CategoryId = contact.CategoryId;
            dbContact.SubcategoryId = contact.SubcategoryId;
            dbContact.UserCategoryId = contact.UserCategoryId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbContacts());
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<Contact>>> DeleteContact(int id)
        {
            var dbContact = await _context.Contacts
                .Include(c => c.Category)
                .Include(s => s.Subcategory)
                .Include(u => u.UserCategory)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (dbContact == null)
            {
                return NotFound("Sorry, but no here contact for you. :/");
            }

            _context.Contacts.Remove(dbContact);
            await _context.SaveChangesAsync();

            return Ok(await GetDbContacts());
        }

        private async Task<List<Contact>> GetDbContacts()
        {
            return await _context.Contacts.Include(c => c.Category).Include(s => s.Subcategory).Include(u => u.UserCategory).ToListAsync();
        }
    }
}
