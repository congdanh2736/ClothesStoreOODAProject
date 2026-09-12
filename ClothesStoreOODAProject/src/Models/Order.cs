using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreOODAProject.src.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public decimal TotalAmount { get; set; } = decimal.Zero;

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        public int? PromotionId { get; set; }
        [ForeignKey("PromotionId")]
        public Promotion? Promotion { get; set; }

        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address? Address { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public PaymentTransaction PaymentTransaction { get; set; } // 1-1 relationship with PaymentTransaction
    }
}
