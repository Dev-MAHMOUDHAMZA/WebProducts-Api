using System.ComponentModel.DataAnnotations;

namespace WebProducts.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
