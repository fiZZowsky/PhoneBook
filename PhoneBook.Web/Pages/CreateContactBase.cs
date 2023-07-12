using Microsoft.AspNetCore.Components;
using PhoneBook.Models.Dtos;
using PhoneBook.Web.Services;
using PhoneBook.Web.Services.Contracts;
using System.Xml.Serialization;

namespace PhoneBook.Web.Pages
{
    public class CreateContactBase : ComponentBase
    {
        [Inject]
        public IContactService contactService { get; set; }

        [Inject]
        public ICategoriesService categoriesService { get; set; }
        
        [Inject]
        public ISubcategoriesService subcategoriesService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public ContactDto contactDto { get; set; } = new ContactDto();

        [Parameter]
        public IEnumerable<CategoriesDto> categories { get; set; }

        [Parameter]
        public IEnumerable<SubcategoriesDto> subcategories { get; set; }

        protected string Message = string.Empty;

        protected CategoriesDto SelectedCategory { get; set; }
        protected SubcategoriesDto SelectedSubcategory { get; set; }


        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            categories = await categoriesService.GetCategories();
            subcategories = await subcategoriesService.GetSubcategories();

            if (string.IsNullOrEmpty(Id))
            {
                // Adding a new contact

            }
            else
            {
                // Updating a new contact
                var contactId = Convert.ToInt32(Id);

                var apiContact = await contactService.GetContact(contactId);

                if (apiContact != null)
                {
                    contactDto = apiContact;
                }
            }
        }

        protected async void HandleFailedRequest()
        {
            Message = "Something went wrond, form not submited.";
        }

        protected async void HandleValidRequest()
        {
            if (string.IsNullOrEmpty(Id))
            {
                // Add contact
                if (SelectedCategory != null && SelectedSubcategory != null)
                {
                    contactDto.CategoryId = SelectedCategory.Id;
                    contactDto.CategoryName = SelectedCategory.CategoryName;
                    contactDto.SubcategoryId = SelectedSubcategory.Id;
                    contactDto.SubcategoryName = SelectedSubcategory.SubcategoryName;

                    var result = await contactService.AddContact(contactDto);

                    if (result != null)
                    {
                        NavigationManager.NavigateTo("../");
                    }
                    else
                    {
                        Message = "Something went wrong, contact not added :(";
                    }
                }
                else
                {
                    Message = "Please select a category and subcategory.";
                }
            }
            else
            {
                // Update contact
                var result = await contactService.UpdateContact(contactDto);

                if (result)
                {
                    NavigationManager.NavigateTo("../");
                }
                else
                {
                    Message = "Something went wrong, contact not updated :(";
                }
            }
        }

        protected void GoToContacts()
        {
            NavigationManager.NavigateTo("../");
        }
    }
}
