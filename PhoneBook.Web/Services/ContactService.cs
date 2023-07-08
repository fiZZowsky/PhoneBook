using PhoneBook.Models.Dtos;
using PhoneBook.Web.Services.Contracts;
using System.Net.Http.Json;

namespace PhoneBook.Web.Services
{
    public class ContactService : IContactService
    {
        private readonly HttpClient httpClient;

        public ContactService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ContactDto> AddContact(ContactDto contact)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<ContactDto>("api/Contact", contact);
            
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.BadRequest) 
                    { 
                        return default(ContactDto);
                    }
                    return await response.Content.ReadFromJsonAsync<ContactDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<ContactDto> DeleteContact(int id)
        {
            throw new NotImplementedException();
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
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<ContactDto>> GetContacts()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Contact");

                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ContactDto>();
                    }
                    else
                    {
                        return await response.Content.ReadFromJsonAsync<IEnumerable<ContactDto>>();
                    }
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
                
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}
