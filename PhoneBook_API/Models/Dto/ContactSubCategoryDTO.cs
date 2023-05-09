using System.ComponentModel.DataAnnotations;

namespace PhoneBook_API.Models.Dto
{
    public class ContactSubcategoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
