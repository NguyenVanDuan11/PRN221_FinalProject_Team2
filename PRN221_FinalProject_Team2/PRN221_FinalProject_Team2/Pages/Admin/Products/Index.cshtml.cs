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

        public void OnGet()
        {
            Products = _db.Products.Include(c => c.Category).ToList();
        }
    }
}
