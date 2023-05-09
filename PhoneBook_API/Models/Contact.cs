using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook_API.Models
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
        [ForeignKey("ContactCategory")]
        public int CategoryId { get; set; }

        public virtual ContactCategory ContactCategory { get; set; }
        public string Subcategory { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [Range(0, 130)]
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get ; set; }
        public DateTime UpdatedDate { get; set;}
    }
}
