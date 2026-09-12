using System.ComponentModel.DataAnnotations;

namespace ClothesStoreOODAProject.src.Models
{
    public class MembershipTier
    {
        [Key]
        public int Id { get; set; }
        public string? TierName { get; set; }

        public ICollection<Customer>? Customers { get; set; } = new List<Customer>();
    }
}
