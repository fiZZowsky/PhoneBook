using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Entities;

namespace PhoneBook.Api.Data
{
    public class PhoneBookDbContext:DbContext
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
    }
}
