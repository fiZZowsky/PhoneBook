using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Api.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Subcategory> Subcategories { get; set; }
        public static List<string> DefaultCategoryNames => new List<string> { "służbowy", "prywatny", "inny" };
    }
}
