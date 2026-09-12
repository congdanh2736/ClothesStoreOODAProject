using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreOODAProject.src.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } = decimal.Zero;

        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart? Cart { get; set; }

        public int VariantId { get; set; }
        [ForeignKey("VariantId")]
        public ProductVariant? ProductVariant { get; set; }
    }
}
