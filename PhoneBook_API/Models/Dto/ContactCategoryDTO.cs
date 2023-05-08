using System.ComponentModel.DataAnnotations;

namespace PhoneBook_API.Models.Dto
{
    public class ContactCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ContactSubcategory> Subcategories { get; set; }
    }
}
