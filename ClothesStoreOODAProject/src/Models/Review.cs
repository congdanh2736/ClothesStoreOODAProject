using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreOODAProject.src.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int Rating { get; set; } = 0;
        public string? Comment { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
