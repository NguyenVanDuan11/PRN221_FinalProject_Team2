using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRN221_FinalProject_Team2.Models;

namespace PRN221_FinalProject_Team2.Pages
{
    public class SignupModel : PageModel
    {
        private readonly PRN221DBContext _context;

        public SignupModel(PRN221DBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Account Account { get; set; }
        public Customer Customer { get; set; }
        // GET: /Signup
        public IActionResult OnGet()
        {
            // Check if the user is already logged in
            string? customer = HttpContext.Session.GetString("customer");
            string? admin = HttpContext.Session.GetString("admin");
            if (customer != null || admin != null)
            {
                return Redirect("/Index");
            }

            ViewData["title"] = "Register";
            return Page();
        }

        // POST: /Signup
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string customerId = Guid.NewGuid().ToString("n").Substring(0, 4).ToUpper();
            var newAcc = new Account()
            {
                Email = Account.Email,
                Password = Account.Password,
                CustomerId = customerId,
                Role = 2
            };

            var newCus = new Customer()
            {
                CustomerId = customerId,
                CompanyName = "Hoa",
                ContactName = null,
                ContactTitle = null,
                Address = null,
            };

            _context.Customers.AddAsync(newCus);
            await _context.SaveChangesAsync();

            newAcc.CustomerId = newCus.CustomerId;

            _context.Accounts.AddAsync(newAcc);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Login");
        }
    }
}