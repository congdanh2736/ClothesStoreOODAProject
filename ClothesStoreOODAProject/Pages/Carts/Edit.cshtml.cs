using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClothesStoreOODAProject.src.Data;
using ClothesStoreOODAProject.src.Models;

namespace ClothesStoreOODAProject.Pages.Carts
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context) { _context = context; }

        [BindProperty]
        public Cart Cart { get; set; } = new Cart();

        public SelectList CustomerList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null) return NotFound();

            Cart = cart;
            CustomerList = new SelectList(await _context.Customers.ToListAsync(), "Id", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CustomerList = new SelectList(await _context.Customers.ToListAsync(), "Id", "FullName");
                return Page();
            }

            _context.Attach(Cart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
