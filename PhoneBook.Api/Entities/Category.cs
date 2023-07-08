using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Api.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public static List<Subcategory> Subcategories { get ; set; }
    }
}
