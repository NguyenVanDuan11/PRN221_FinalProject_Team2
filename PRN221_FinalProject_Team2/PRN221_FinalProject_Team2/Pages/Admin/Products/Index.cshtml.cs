using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject_Team2.Models;

namespace PRN221_FinalProject_Team2.Pages.Admin.Products
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly PRN221DBContext _db;

        public IndexModel(PRN221DBContext db)
        {
            _db = db;
        }

        public List<Product> Products { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Products = await _db.Products.Include(c => c.Category).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetDelete(int id)
        {
            var checkExist = _db.OrderDetails.FirstOrDefault(od => od.ProductId == id);
            if(checkExist != null)
            {
                TempData["Exist"] = "Product is currently being used, can't delete the product!";
                return RedirectToPage("Index");
            }
            var product = await _db.Products.FindAsync(id);
            if(product != null)
            {
				TempData["Success"] = "Delete successfully!";
				_db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
