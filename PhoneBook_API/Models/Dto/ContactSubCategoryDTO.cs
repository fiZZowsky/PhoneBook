using System.ComponentModel.DataAnnotations;

namespace PhoneBook_API.Models.Dto
{
    public class ContactSubCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public ContactCategory Category { get; set; }
    }
}
