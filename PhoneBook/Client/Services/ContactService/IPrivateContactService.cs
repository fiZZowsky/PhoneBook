namespace PhoneBook.Client.Services.ContactService
{
    public interface IPrivateContactService
    {
        List<Contact> Contacts { get; set; }
        List<Category> Categories { get; set; }
        List<Subcategory> Subcategories { get; set; }
        List<UserCategory> UserCategories { get; set; }

        Task GetContacts();
        Task<Contact> GetContact(int id);
        Task GetCategories();
        Task GetSubcategories();
        Task GetUserCategories();
        Task CreateContact(Contact contact);
        Task CreateNewCategory(UserCategory userCategory);
        Task UpdateContact(Contact contact);
        Task DeleteContact(int id);
    }
}
