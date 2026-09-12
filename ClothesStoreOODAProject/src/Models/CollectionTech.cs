using System.ComponentModel.DataAnnotations;

namespace ClothesStoreOODAProject.src.Models
{
    public class CollectionTech
    {
        [Key]
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }

        // Navigation
        public ICollection<ProductCollection>? ProductCollections { get; set; } = new List<ProductCollection>();
    }
}
