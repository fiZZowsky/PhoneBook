using PhoneBook.Models.Dtos;
using PhoneBook.Web.Services.Contracts;
using System.Net.Http.Json;

namespace PhoneBook.Web.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient httpClient;

        public CategoriesService(HttpClient httpClient)
        {

            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoriesDto>?> GetCategories()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Categories");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CategoriesDto>();
                    }
                    else
                    {
                        return await response.Content.ReadFromJsonAsync<IEnumerable<CategoriesDto>>();
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

        public async Task<CategoriesDto> GetCategory(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"api/Categories/{id}");


                if (response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CategoriesDto);
                    }

                    return await response.Content.ReadFromJsonAsync<CategoriesDto>();
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
