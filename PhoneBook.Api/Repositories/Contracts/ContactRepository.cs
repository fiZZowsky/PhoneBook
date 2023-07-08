using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Data;
using PhoneBook.Api.Entities;
using PhoneBook.Models.Dtos;

namespace PhoneBook.Api.Repositories.Contracts
{
    public class ContactRepository : IContactRepository
    {
        private readonly PhoneBookDbContext phoneBookDbContext;

        public ContactRepository(PhoneBookDbContext phoneBookDbContext)
        {
            this.phoneBookDbContext = phoneBookDbContext;
        }
        private async Task<bool> emailExists(string email)
        {
            return await this.phoneBookDbContext.Contacts.AnyAsync(e => e.Email == email);
        }
        public async Task<Contact> AddContact(ContactDto contactDto)
        {
            if(await emailExists(contactDto.Email) == false)
            {
                Contact newContact = new()
                {
                    Id = contactDto.Id,
                    FirstName = contactDto.FirstName,
                    LastName = contactDto.LastName,
                    Email = contactDto.Email,
                    Password = contactDto.Password,
                    PhoneNumber = contactDto.PhoneNumber,
                    BirthDate = contactDto.BirthDate,
                    CategoryId = contactDto.CategoryId,
                    SubcategoryId = contactDto.SubcategoryId
                };

                await this.phoneBookDbContext.AddAsync(newContact);
                await this.phoneBookDbContext.SaveChangesAsync();

                return newContact;
            }
            return null;
        }

        public async Task<bool> DeleteContact(int Id)
        {
            var contact = await phoneBookDbContext.Contacts.SingleOrDefaultAsync(c => c.Id == Id);

            if (contact == null)
            {
                return false;
            }

            phoneBookDbContext.Contacts.Remove(contact);
            await phoneBookDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await this.phoneBookDbContext.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await phoneBookDbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Contact> GetContact(int id)
        {
            var contact = await phoneBookDbContext.Contacts.FindAsync(id);

            return contact;
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var contacts = await this.phoneBookDbContext.Contacts.ToListAsync();
            
            return contacts;
        }

        public async Task<IEnumerable<Subcategory?>> GetSubcategories()
        {
            var subcategories = await this.phoneBookDbContext.Subcategories.ToListAsync();

            return subcategories;
        }

        public async Task<Subcategory?> GetSubcategory(int id)
        {
            var subcategory = await phoneBookDbContext.Subcategories.SingleOrDefaultAsync(s => s.Id == id);
            return subcategory;
        }

        public async Task<Contact> UpdateContact(int id, ContactDto contactDto)
        {
            var existingContact = await this.phoneBookDbContext.Contacts.FindAsync(id);

            if (existingContact == null)
            {
                return null;
            }

            existingContact.FirstName = contactDto.FirstName;
            existingContact.LastName = contactDto.LastName;
            existingContact.Email = contactDto.Email;
            existingContact.Password = contactDto.Password;
            existingContact.PhoneNumber = contactDto.PhoneNumber;
            existingContact.BirthDate = contactDto.BirthDate;
            existingContact.CategoryId = contactDto.CategoryId;
            existingContact.SubcategoryId = contactDto.SubcategoryId;

            await this.phoneBookDbContext.SaveChangesAsync();

            return existingContact;
        }

    }
}
