using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Api.Entities
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int CategoryId { get; set; }
    }
}
