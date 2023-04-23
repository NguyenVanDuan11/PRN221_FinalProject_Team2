using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject_Team2.Models;
using System;
using System.Diagnostics;
using System.Text.Json;

namespace PRN221_FinalProject_Team2.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly PRN221DBContext _db;

        public EditProfileModel(PRN221DBContext db)
        {
            _db = db;
            Customer = new Customer();
        }
        [BindProperty]
        public Account Account { get; set; }
        public Customer Customer { get; set; }


        public IActionResult OnGet()
        {
            //string? email = HttpContext.Session.GetString("email");
            string? customer = HttpContext.Session.GetString("customer");
            string? admin = HttpContext.Session.GetString("admin");
            if (customer == null || admin != null)
            {
                return NotFound();
            }
            Account acc = JsonSerializer.Deserialize<Account>(customer);
            Account = _db.Accounts.Include(x => x.Customer).FirstOrDefault(x => x.Email == acc.Email);
            if (Account == null)
            {
                return NotFound();
            }

            Customer = Account.Customer;
            return Page();
        }
        public IActionResult OnPost()
        {
            

            return Page();
        }
    }
}
