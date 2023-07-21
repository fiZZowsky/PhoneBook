using Microsoft.AspNetCore.Components.Authorization;
using PhoneBook.Shared;

namespace PhoneBook.Client.Services.ContactService
{
    public interface IContactService
    {
        List<Contact> Contacts { get; set; }
        List<Category> Categories { get; set; }
        List<Subcategory> Subcategories { get; set; }

        List<UserCategory> UserCategories { get; set; }

        Task GetContacts();
        Task GetCategories();
        Task GetSubcategories();
        Task GetUserCategories();
        Task<Contact> GetContact(int id);
        Task CreateContact(Contact contact);
        Task CreateNewCategory(UserCategory userCategory);
        Task UpdateContact(Contact contact);
        Task DeleteContact(int id);
    }
}
