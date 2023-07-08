using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Api.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
    }
}
