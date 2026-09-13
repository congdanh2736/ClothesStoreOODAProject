using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ClothesStoreOODAProject.src.Data;
using ClothesStoreOODAProject.src.Models;

namespace ClothesStoreOODAProject.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context) { _context = context; }

        [BindProperty]
        public Order Order { get; set; } = new Order();

        public SelectList CustomerList { get; set; } = default!;
        public SelectList AddressList { get; set; } = default!;
        public SelectList PromotionList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            await LoadDropdownsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return Page();
            }

            _context.Orders.Add(Order);
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
