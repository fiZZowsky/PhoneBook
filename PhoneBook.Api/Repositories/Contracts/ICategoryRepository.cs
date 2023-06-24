using PhoneBook.Api.Entities;

namespace PhoneBook.Api.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int Id);
    }
}
