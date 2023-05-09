using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook_API.Models
{
    public class ContactSubcategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("ContactCategory")]
        public int CategoryId { get; set; }

        public virtual ContactCategory ContactCategory { get; set; }
    }
}
