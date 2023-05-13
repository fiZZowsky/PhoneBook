using Microsoft.AspNetCore.Components;
using PhoneBook.Models.Dtos;
using PhoneBook.Web.Services.Contracts;

namespace PhoneBook.Web.Pages
{
    public class ContactsBase:ComponentBase
    {
        [Inject]
        public IContactService ContactService { get; set; }
   
        public IEnumerable<ContactDto> Contacts { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Contacts = await ContactService.GetContacts();
        }

        protected async Task AddNewContact_Click(ContactDto contactDto)
        {
            try
            {
                var newContactDto = await ContactService.AddContact(contactDto);
            }
            catch (Exception)
            {
                //Log exception
            }
        }

    }
}
