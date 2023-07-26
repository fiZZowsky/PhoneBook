using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Shared
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Contact name not provided")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Contact last name not provided")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 40 characters")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Contact email not provided")]
        [EmailAddress(ErrorMessage = "Invalid email syntax")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Contact phone number not provided")]
        [Phone(ErrorMessage = "Invalid phone number syntax")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Contact date of birth not provided")]
        [FutureDate(ErrorMessage = "The Birth Date field cannot be a future date.")]
        public DateTime BirthDate { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "Contact category not provided")]
        [Range(1, int.MaxValue, ErrorMessage = "No category selected")]
        public int CategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }
        public int? SubcategoryId { get; set; }
        public UserCategory? UserCategory { get; set; }
        public int? UserCategoryId { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date <= DateTime.Now;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field cannot be a future date.";
        }
    }
}
