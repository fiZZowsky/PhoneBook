using PhoneBook.Api.Entities;

namespace PhoneBook.Api.Repositories.Contracts
{
    public interface ISubcategoriesRepository
    {
        Task<IEnumerable<Subcategory>> GetSubcategories();
        Task<Subcategory> GetSubcategory(int id);
    }
}
