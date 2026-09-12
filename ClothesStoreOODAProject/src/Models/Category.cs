using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreOODAProject.src.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public Category? Parent { get; set; }

        // Navigation
        public ICollection<Category>? Children { get; set; } = new List<Category>();
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
