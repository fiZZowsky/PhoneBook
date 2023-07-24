using System.Net.Http.Json;

namespace PhoneBook.Client.Services.ContactService
{
    public class PublicContactService
    {
        private HttpClient _client;

        public PublicContactService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public List<Contact> Contacts { get; set; } = new List<Contact>();

        public async Task GetContacts()
        {
            try
            {
                var result = await _client.GetFromJsonAsync<List<Contact>>("api/contact");
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
    }
}
