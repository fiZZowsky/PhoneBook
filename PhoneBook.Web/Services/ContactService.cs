using PhoneBook.Models.Dtos;
using PhoneBook.Web.Services.Contracts;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PhoneBook.Web.Services
{
    public class ContactService : IContactService
    {
        private readonly HttpClient httpClient;

        public ContactService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ContactDto?> AddContact(ContactDto contact)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("api/Contact", contact);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        return null;
                    }

                    var createdContact = await response.Content.ReadFromJsonAsync<ContactDto>();

                    return createdContact;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"HTTP status: {response.StatusCode}, Message: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteContact(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Contact/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> UpdateContact(ContactDto contact)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(contact), Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"api/Contact/{contact.Id}", itemJson);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<ContactDto> GetContact(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Contact/{id}");

                if(response.IsSuccessStatusCode) 
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ContactDto);
                    }

                    return await response.Content.ReadFromJsonAsync<ContactDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<List<ContactDto>?> GetContacts()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Contact");

                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ContactDto>().ToList();
                    }
                    else
                    {
                        return await response.Content.ReadFromJsonAsync<List<ContactDto>>();
                    }
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }
    }
}
