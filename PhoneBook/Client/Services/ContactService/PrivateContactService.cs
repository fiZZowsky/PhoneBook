using Microsoft.AspNetCore.Components;
using PhoneBook.Shared;
using System.Net.Http.Json;

namespace PhoneBook.Client.Services.ContactService
{
    public class PrivateContactService : IPrivateContactService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public PrivateContactService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
        public List<UserCategory> UserCategories { get; set; } = new List<UserCategory>();

        public async Task SetContacts(HttpResponseMessage result)
        {
            await GetContacts();
            var response = await result.Content.ReadFromJsonAsync<List<Contact>>();
            Contacts = response;
            _navigationManager.NavigateTo("/contacts");
        }

        public async Task GetContacts()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<Contact>>("api/contact");
                if (result != null)
                {
                    Contacts = result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task CreateContact(Contact contact)
        {
            var result = await _httpClient.PostAsJsonAsync("api/contact/contact", contact);
            await SetContacts(result);
        }

        public async Task DeleteContact(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/contact/contact/{id}");
            await SetContacts(result);
        }

        public async Task<Contact> GetContact(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Contact>($"api/contact/contact/{id}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("Contact not found!");
        }

        public async Task UpdateContact(Contact contact)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/contact/contact/{contact.Id}", contact);
            await SetContacts(result);
        }

        public async Task<int> CreateNewCategory(UserCategory userCategory)
        {
            var response = await _httpClient.PostAsJsonAsync("api/contact/usercategory", userCategory);

            int newCategoryId = await response.Content.ReadFromJsonAsync<int>();
            return newCategoryId;
        }

        public async Task GetCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Category>>("api/contact/categories");
            Console.WriteLine(result);
            if (result != null)
            {
                Categories = result;
            }
        }

        public async Task GetSubcategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Subcategory>>("api/contact/subcategories");
            if (result != null)
            {
                Subcategories = result;
            }
        }

        public async Task GetUserCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserCategory>>("api/contact/usercategories");
            if (result != null)
            {
                UserCategories = result;
            }
        }

        public async Task UpdateUserCategory(UserCategory userCategory)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/contact/usercategory/{userCategory.Id}", userCategory);
        }

        public async Task DeleteUserCategory(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/contact/usercategory/{id}");
        }
    }
}
