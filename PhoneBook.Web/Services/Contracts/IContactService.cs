using PhoneBook.Models.Dtos;

namespace PhoneBook.Web.Services.Contracts
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetContacts();
        Task<ContactDto> GetContact(int id);
        Task<ContactDto> AddContact(ContactDto contact);
        Task<ContactDto> DeleteContact(int id);
    }
}
