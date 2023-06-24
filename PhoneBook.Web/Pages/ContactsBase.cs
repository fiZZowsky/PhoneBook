using Microsoft.AspNetCore.Components;
using PhoneBook.Models.Dtos;
using PhoneBook.Web.Services.Contracts;

namespace PhoneBook.Web.Pages
{
    public class ContactsBase:ComponentBase
    {
        [Inject]
        public IContactService ContactService { get; set; }
        //[Inject]
        //public ICategoryService CategoryService { get; set; }
   
        public IEnumerable<ContactDto> Contacts { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Contacts = await ContactService.GetContacts();
            //Categories = await CategoryService.GetCategories();
        }

        protected async Task AddNewContact_Click(/*ContactDto contactDto*/)
        {
            //try
            //{
            //    var newContactDto = await ContactService.AddContact(contactDto);
            //}
            //catch (Exception)
            //{
            //    //Log exception
            //}
        }

    }
}
