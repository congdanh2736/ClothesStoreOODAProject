using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreOODAProject.src.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int TierId { get; set; }
        [ForeignKey("TierId")]
        public MembershipTier? MembershipTier { get; set; }

        // Navigation
        public ICollection<Order>? Orders { get; set; } = new List<Order>();
        public ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public Cart? Cart { get; set; }
        public ICollection<Address>? Addresses { get; set; } = new List<Address>();
    }
}
