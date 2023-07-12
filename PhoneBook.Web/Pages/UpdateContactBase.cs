using Microsoft.AspNetCore.Components;
using PhoneBook.Models.Dtos;
using PhoneBook.Web.Services.Contracts;

namespace PhoneBook.Web.Pages
{
    public class UpdateContactBase : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IContactService ContactService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ContactDto Contact { get; set; }

        protected string Message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var contactId = Convert.ToInt32(Id);
                Contact = await ContactService.GetContact(contactId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void HandleInvalidRequest()
        {
            Message = "Something went wrong, contact not submited.";
        }

        protected async void HandleValidRequest()
        {
            var result = await ContactService.UpdateContact(Contact);

            if(result)
            {
                NavigationManager.NavigateTo("../");
            }
            else
            {
                Message = "Something went wrong, contact not updated.";
            }
        }

        protected void GoToContactDetails()
        {
            NavigationManager.NavigateTo("../");
        }
    }
}
