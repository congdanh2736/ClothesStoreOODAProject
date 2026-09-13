using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClothesStoreOODAProject.src.Data;
using ClothesStoreOODAProject.src.Models;

namespace ClothesStoreOODAProject.Pages.Carts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) { _context = context; }

        public IList<Cart> Carts { get; set; } = new List<Cart>();

        public async Task OnGetAsync()
        {
            Carts = await _context.Carts
                .Include(c => c.Customer)
                .Include(c => c.CartItems)
                .ToListAsync();
        }
    }
}
