using PhoneBook.Models.Dtos;

namespace PhoneBook.Web.Services.Contracts
{
    public interface IContactService
    {
        Task<List<ContactDto>?> GetContacts();
        Task<ContactDto?> GetContact(int id);
        Task<ContactDto?> AddContact(ContactDto contact);
        Task<bool> UpdateContact(ContactDto contact);
        Task<bool> DeleteContact(int id);
    }
}
