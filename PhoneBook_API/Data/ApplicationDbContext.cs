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
        public DbSet<ContactCategory> ContactCategories { get; set; }
        public DbSet<ContactSubcategory> ContactSubcategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Contact>().HasData(
            //    new Contact()
            //    {
            //        Id = 1,
            //        FirstName = "Jan",
            //        LastName = "Kowalski",
            //        Email = "example1@email.com",
            //        Password = "password",
            //        PhoneNumber = "123456789",
            //        BirhDate = new DateTime(1989, 7, 20).Date,
            //        CreatedDate = DateTime.Now,
            //    },
            //    new Contact()
            //    {
            //        Id = 2,
            //        FirstName = "Anna",
            //        LastName = "Nowak",
            //        Email = "example2@mail.com",
            //        Password = "password2",
            //        PhoneNumber = "987654321",
            //        BirhDate = new DateTime(1995, 3, 15).Date,
            //        CreatedDate = DateTime.Now,
            //    }
            //    ) ;
        }
    }
}
