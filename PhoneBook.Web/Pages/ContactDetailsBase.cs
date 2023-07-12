using Microsoft.AspNetCore.Components;
using PhoneBook.Models.Dtos;
using PhoneBook.Web.Services.Contracts;

namespace PhoneBook.Web.Pages
{
    public class ContactDetailsBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IContactService ContactService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public List<ContactDto> Contacts { get; set; }

        public ContactDto Contact { get; set; }
        public string ErrorMessage { get; set; }

        protected string Message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var contactId = Convert.ToInt32(Id);
                Contact = await ContactService.GetContact(contactId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private ContactDto GetContact(int id)
        {
            return Contacts.FirstOrDefault(i => i.Id == id);
        }

        public async void RemoveContact()
        {
            var contactId = Convert.ToString(Id);
            if (!string.IsNullOrEmpty(contactId))
            {
                var result = await ContactService.DeleteContact(Id);

                if (result)
                {
                    NavigationManager.NavigateTo("../");
                }
                else
                {
                    Message = "Something went wrong, contact not deleted :( )";
                }
            }
        }
    }
}
