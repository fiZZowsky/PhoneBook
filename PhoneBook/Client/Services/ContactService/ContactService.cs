using Microsoft.AspNetCore.Components;
using PhoneBook.Client.Pages;
using PhoneBook.Shared;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace PhoneBook.Client.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public ContactService(HttpClient httpClient, NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
        public List<UserCategory> UserCategories { get; set; } = new List<UserCategory>();

        public async Task CreateContact(Contact contact)
        {
            var result = await _httpClient.PostAsJsonAsync("api/contact", contact);
            await SetContacts(result);
        }

        private async Task SetContacts(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Contact>>();
            Contacts = response;
            _navigationManager.NavigateTo("contacts");
        }

        public async Task DeleteContact(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/contact/{id}");
            await SetContacts(result);
        }

        public async Task GetCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Category>>("api/contact/categories");
            if (result != null)
            {
                Categories = result;
            }
        }

        public async Task<Contact> GetContact(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Contact>($"api/contact/{id}");
            if (result != null)
            {
                return result;
            }
            throw new Exception("Contact not found!");
        }
        public async Task GetContacts()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity.IsAuthenticated)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetAccessToken(authState));
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
                    // Obsłuż ewentualne błędy podczas pobierania kontaktów
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private string GetAccessToken(AuthenticationState authState)
        {
            var token = authState.User.FindFirst("access_token")?.Value;
            return token;
        }

        public async Task GetSubcategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Subcategory>>("api/contact/subcategories");
            if (result != null)
            {
                Subcategories = result;
            }
        }

        public async Task UpdateContact(Contact contact)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/contact/{contact.Id}", contact);
            await SetContacts(result);
        }

        public async Task GetUserCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserCategory>>("api/contact/usercategories");
            if (result != null)
            {
                UserCategories = result;
            }
        }

        public async Task CreateNewCategory(UserCategory userCategory)
        {
            var result = await _httpClient.PostAsJsonAsync("api/contact/usercategories", userCategory);
            await SetContacts(result);
        }
    }
}
