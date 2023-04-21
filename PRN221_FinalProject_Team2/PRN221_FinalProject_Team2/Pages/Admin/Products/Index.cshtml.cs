using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_FinalProject_Team2.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToPage("/Admin/Categories/Index");
            }
            else
            {
                return RedirectToPage("/Admin/Products/Index");
            }
        }
    }
}
