using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Shared
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }
        public int? SubcategoryId { get; set; }
        public UserCategory? UserCategory { get; set; }
        public int? UserCategoryId { get; set; }
    }
}
