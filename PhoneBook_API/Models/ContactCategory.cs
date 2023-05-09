using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook_API.Models
{
    public class ContactCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<ContactSubcategory> ContactSubcategories { get; set; }

        public static List<string> DefaultCategoryNames => new List<string> { "służbowy", "prywatny" };
    }
}