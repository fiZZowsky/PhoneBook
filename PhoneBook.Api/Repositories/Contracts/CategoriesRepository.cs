using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Data;
using PhoneBook.Api.Entities;

namespace PhoneBook.Api.Repositories.Contracts
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly PhoneBookDbContext phoneBookDbContext;

        public CategoriesRepository(PhoneBookDbContext phoneBookDbContext)
        {
            this.phoneBookDbContext = phoneBookDbContext;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await this.phoneBookDbContext.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await phoneBookDbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }
    }
}
