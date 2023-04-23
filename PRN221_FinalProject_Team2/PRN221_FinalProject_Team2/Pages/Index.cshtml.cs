using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject_Team2.Models;

namespace PRN221_FinalProject_Team2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PRN221DBContext _db;

        public IndexModel(PRN221DBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public List<Product> products { get; set; } = new List<Product>();

        [BindProperty]
        public int page { get; set; }

        [BindProperty]
        public int numberPage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            int pageSize = 8;
            pageIndex = pageIndex ?? 1;
            page = pageIndex.Value;
            int total = _db.Products
                .Include(x => x.Category)
                .Where(x => x.Discontinued == false)
                .Count();
            numberPage = (int)Math.Ceiling((double)total / (double)pageSize);
            products = _db.Products
                .Include(x => x.Category)
                .Where(x => x.Discontinued == false)
                .Skip((pageIndex.Value - 1) * pageSize)
                .Take(pageSize)                
                .ToList();

            var a = Convert.ToBase64String(products.First().Category.Picture);
            return Page();
        }
    }
}