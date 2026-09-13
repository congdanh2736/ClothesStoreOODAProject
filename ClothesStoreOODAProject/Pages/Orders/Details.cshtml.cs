using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClothesStoreOODAProject.src.Data;
using ClothesStoreOODAProject.src.Models;

namespace ClothesStoreOODAProject.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DetailsModel(ApplicationDbContext context) { _context = context; }

        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Address)
                .Include(o => o.Promotion)
                .Include(o => o.OrderItems)
                .Include(o => o.PaymentTransaction)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();
            Order = order;
            return Page();
        }
    }
}
