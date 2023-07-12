using PhoneBook.Api.Entities;

namespace PhoneBook.Api.Repositories.Contracts
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
    }
}
