using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreOODAProject.src.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        // Navigation
        public ICollection<ProductVariant>? ProductVariants { get; set; } = new List<ProductVariant>();
        public ICollection<ProductCollection>? ProductCollections { get; set; } = new List<ProductCollection>();
        public ICollection<ProductImage>? ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<Wishlist>? Wishlists { get; set; } = new List<Wishlist>();
        public ICollection<Review>? Reviews { get; set; } = new List<Review>();
    }
}
