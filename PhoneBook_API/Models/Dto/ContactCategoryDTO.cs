using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook_API.Models.Dto
{
    public class ContactCategoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<ContactSubcategoryDTO> Subcategories { get; set; }
    }
}
