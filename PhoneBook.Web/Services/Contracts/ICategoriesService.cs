using PhoneBook.Models.Dtos;

namespace PhoneBook.Web.Services.Contracts
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoriesDto>?> GetCategories();
        Task<CategoriesDto?> GetCategory(int id);
    }
}
