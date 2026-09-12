using System.ComponentModel.DataAnnotations;

namespace ClothesStoreOODAProject.src.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }
        public string? Code { get; set; }
        public decimal DiscountValue { get; set; } = decimal.Zero;

        // Navigation
        public ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
