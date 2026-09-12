using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStoreOODAProject.src.Models
{
    public class ProductCollection
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

        public int CollectionId { get; set; }
        [ForeignKey("CollectionId")]
        public CollectionTech? CollectionTech { get; set; }
    }
}
