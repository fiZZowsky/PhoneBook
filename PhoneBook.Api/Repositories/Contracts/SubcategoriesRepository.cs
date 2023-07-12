using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Data;
using PhoneBook.Api.Entities;

namespace PhoneBook.Api.Repositories.Contracts
{
    public class SubcategoriesRepository : ISubcategoriesRepository
    {
        private readonly PhoneBookDbContext phoneBookDbContext;

        public SubcategoriesRepository(PhoneBookDbContext phoneBookDbContext)
        {
            this.phoneBookDbContext = phoneBookDbContext;
        }

        public async Task<IEnumerable<Subcategory>> GetSubcategories()
        {
            var subcategories = await this.phoneBookDbContext.Subcategories.ToListAsync();

            return subcategories;
        }

        public async Task<Subcategory> GetSubcategory(int id)
        {
            var subcategory = await phoneBookDbContext.Subcategories.SingleOrDefaultAsync(s => s.Id == id);
            return subcategory;
        }
    }
}
