using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClothesStoreOODAProject.src.Data;
using ClothesStoreOODAProject.src.Models;

namespace ClothesStoreOODAProject.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context) { _context = context; }

        [BindProperty]
        public Order Order { get; set; } = new Order();

        public SelectList CustomerList { get; set; } = default!;
        public SelectList AddressList { get; set; } = default!;
        public SelectList PromotionList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            Order = order;
            await LoadDropdownsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return Page();
            }

            _context.Attach(Order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        private async Task LoadDropdownsAsync()
        {
            CustomerList = new SelectList(await _context.Customers.ToListAsync(), "Id", "FullName");
            AddressList = new SelectList(await _context.Addresses.ToListAsync(), "Id", "FullAddress");
            PromotionList = new SelectList(await _context.Promotions.ToListAsync(), "Id", "Code");
        }
    }
}
