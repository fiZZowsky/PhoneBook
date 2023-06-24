//using PhoneBook.Models.Dtos;
//using PhoneBook.Web.Services.Contracts;
//using System.Net.Http.Json;

//namespace PhoneBook.Web.Services
//{
//    public class CategoryService : ICategoryService
//    {
//        private readonly HttpClient httpClient;

//        public CategoryService(HttpClient httpClient)
//        {
//            this.httpClient = httpClient;
//        }

//        public async Task<IEnumerable<CategoryDto>> GetCategories()
//        {
//            try
//            {
//                var response = await this.httpClient.GetAsync("api/Category");
//                if (response.IsSuccessStatusCode)
//                {
//                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
//                    {
//                        return Enumerable.Empty<CategoryDto>();
//                    }
//                    return await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>();
//                }
//                else
//                {
//                    var message = await response.Content.ReadAsStringAsync();
//                    throw new Exception(message);
//                }
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }
//    }
//}
