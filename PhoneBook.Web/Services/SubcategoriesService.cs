using PhoneBook.Models.Dtos;
using System.Net.Http.Json;

namespace PhoneBook.Web.Services.Contracts
{
    public class SubcategoriesService : ISubcategoriesService
    {
        private readonly HttpClient httpClient;

        public SubcategoriesService(HttpClient httpClient)
        {

            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<SubcategoriesDto>> GetSubcategories()
        {
            try
            {
                var response = await this.httpClient.GetAsync("api/Subcategories");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<SubcategoriesDto>();
                    }
                    else
                    {
                        return await response.Content.ReadFromJsonAsync<IEnumerable<SubcategoriesDto>>();
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
