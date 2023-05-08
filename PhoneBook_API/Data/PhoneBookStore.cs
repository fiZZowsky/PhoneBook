using PhoneBook_API.Models.Dto;

namespace PhoneBook_API.Data
{
    public static class PhoneBookStore
    {
        public static List<ContactDTO> contactsList = new List<ContactDTO> {
                new ContactDTO { Id = 1, FirstName="Michal", LastName="Konwiak", Email="jamnikxd@gmail.com", Password="Jamnik677"},
                new ContactDTO { Id = 2, FirstName="Michal", LastName="Pyl", Email="michaldust@interia.pl", Password="pyl"}
            };
    }
}
