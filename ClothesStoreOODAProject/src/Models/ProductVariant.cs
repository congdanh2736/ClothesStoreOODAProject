using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreOODAProject.src.Models
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        // Navigation
        public ICollection<StoreStock>? StoreStocks { get; set; } = new List<StoreStock>();
        public ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();   
        public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<StoreItemStat>? StoreItemStats { get; set; } = new List<StoreItemStat>();
    }
}
