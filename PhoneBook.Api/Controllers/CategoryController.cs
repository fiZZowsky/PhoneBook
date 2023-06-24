//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using PhoneBook.Api.Repositories.Contracts;
//using PhoneBook.Api.Extensions;
//using PhoneBook.Models.Dtos;

//namespace PhoneBook.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoryController : ControllerBase
//    {
//        private readonly ICategoryRepository categoryRepository;

//        public CategoryController(ICategoryRepository categoryRepository)
//        {
//            this.categoryRepository = categoryRepository;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
//        {
//            try
//            {
//                var categories = await this.categoryRepository.GetCategories();
//                if (categories == null)
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    var categoriesDtos = categories.ConvertToDto().ToList();


//                    return Ok(categoriesDtos);
//                }
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }
//    }
//}
