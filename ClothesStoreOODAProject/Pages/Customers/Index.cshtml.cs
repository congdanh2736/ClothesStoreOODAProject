using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClothesStoreOODAProject.src.Data;
using ClothesStoreOODAProject.src.Models;

namespace ClothesStoreOODAProject.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) { _context = context; }

        public IList<Customer> Customers { get; set; } = new List<Customer>();

        public async Task OnGetAsync()
        {
            Customers = await _context.Customers
                .Include(c => c.MembershipTier)
                .ToListAsync();
        }
    }
}
