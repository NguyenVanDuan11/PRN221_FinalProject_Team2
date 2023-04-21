using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_FinalProject_Team2.Models;

namespace PRN221_FinalProject_Team2.Pages.Admin.Categories
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly PRN221DBContext _db;

        public IndexModel(PRN221DBContext db)
        {
            _db = db;
        }

        public List<Category> Categories { get; set; }

        public void OnGet()
        {
            Categories = _db.Categories.ToList();
        }
    }
}
