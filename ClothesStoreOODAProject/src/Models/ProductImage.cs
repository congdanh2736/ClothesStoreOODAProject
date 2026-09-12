using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreOODAProject.src.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
