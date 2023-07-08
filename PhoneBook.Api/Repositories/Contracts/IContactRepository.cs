using PhoneBook.Api.Entities;
using PhoneBook.Models.Dtos;

namespace PhoneBook.Api.Repositories.Contracts
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Subcategory>> GetSubcategories();
        Task<Contact> GetContact(int id);
        Task<Category> GetCategory(int id);
        Task<Subcategory> GetSubcategory(int id);

        Task<Contact> AddContact(ContactDto contactDto);
        Task<Contact> UpdateContact(int Id, ContactDto contactDto);
        Task<bool> DeleteContact(int Id);
    }
}
