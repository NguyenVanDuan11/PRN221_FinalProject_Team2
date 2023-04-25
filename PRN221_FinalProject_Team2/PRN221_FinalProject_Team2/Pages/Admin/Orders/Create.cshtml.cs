using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject_Team2.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace PRN221_FinalProject_Team2.Pages.Admin.Orders
{
    public class CreateModel : PageModel
    {
        private readonly PRN221_FinalProject_Team2.Models.PRN221DBContext _db;

        public CreateModel(PRN221_FinalProject_Team2.Models.PRN221DBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            _db.Orders.Add(Order);
            try
            {
                await _db.SaveChangesAsync();
            }catch (Exception ex)
            {
                ViewData["err"] = ex.ToString();
                return Page();
            }
            return RedirectToPage("./Index");
        }


        
    }
}
