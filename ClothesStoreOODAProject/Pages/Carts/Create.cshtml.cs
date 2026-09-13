using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClothesStoreOODAProject.src.Data;
using ClothesStoreOODAProject.src.Models;

namespace ClothesStoreOODAProject.Pages.Carts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context) { _context = context; }

        [BindProperty]
        public Cart Cart { get; set; } = new Cart();

        public SelectList CustomerList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Chi hien thi cac customer chua co cart (quan he 1-1)
            var customerIdsWithCart = _context.Carts.Select(c => c.CustomerId);
            var customers = await _context.Customers
                .Where(c => !customerIdsWithCart.Contains(c.Id))
                .ToListAsync();

            CustomerList = new SelectList(customers, "Id", "FullName");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CustomerList = new SelectList(await _context.Customers.ToListAsync(), "Id", "FullName");
                return Page();
            }

            _context.Carts.Add(Cart);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
