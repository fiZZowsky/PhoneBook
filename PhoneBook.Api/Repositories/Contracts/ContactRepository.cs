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

        //public Task<Subcategory> AddSubcategory(SubcategoryDto subcategoryDto)
        //{
        //    Subcategory newSubcategory = new()
        //    {
        //        Id = subcategoryDto.Id,
        //        Name = subcategoryDto.Name,
        //        CategoryId = subcategoryDto.CategoryId
        //    };

        //    if(newSubcategory != null)
        //    {
        //        await this.phoneBookDbContext.AddAsync(newSubcategory);
        //        await this.phoneBookDbContext.SaveChangesAsync();
        //        return newSubcategory;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

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

        //public Task<Subcategory> DeleteSubcategory(int Id)
        //{
        //    throw new NotImplementedException();
        //}

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

        public Task<Contact> UpdateContact(int Id, ContactDto contactDto)
        {
            throw new NotImplementedException();
        }

        //public Task<Subcategory> UpdateSubcategory(int Id, SubcategoryDto subcategoryDto)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
