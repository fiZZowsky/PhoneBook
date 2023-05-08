using Microsoft.EntityFrameworkCore;
using PhoneBook_API.Models;

namespace PhoneBook_API.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            :base(options)
        { 
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
