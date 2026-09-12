using System.ComponentModel.DataAnnotations;

namespace ClothesStoreOODAProject.src.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }

        // Navigation
        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
        public ICollection<StoreStock>? StoreStocks { get; set; } = new List<StoreStock>();
        public ICollection<StoreItemStat>? StoreItemStats { get; set; } = new List<StoreItemStat>();
        public ICollection<StoreDailyStat>? StoreDailyStats { get; set; } = new List<StoreDailyStat>();
    }
}
