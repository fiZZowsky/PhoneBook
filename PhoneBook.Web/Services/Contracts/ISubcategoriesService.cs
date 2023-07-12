using PhoneBook.Models.Dtos;

namespace PhoneBook.Web.Services.Contracts
{
    public interface ISubcategoriesService
    {
        Task<IEnumerable<SubcategoriesDto>> GetSubcategories();
    }
}
