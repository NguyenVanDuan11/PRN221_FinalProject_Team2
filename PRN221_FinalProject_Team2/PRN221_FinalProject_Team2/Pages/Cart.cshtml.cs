using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_FinalProject_Team2.Models;
using System.Text.Json;

namespace PRN221_FinalProject_Team2.Pages
{
    public class CartModel : PageModel
    {
		private readonly PRN221DBContext _db;

		public CartModel(PRN221DBContext db)
		{
			_db = db;
		}

		[BindProperty]
		public List<CartItem> cartItems { get; set; } = new List<CartItem>();
		public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("Account") == null)
            {
                return RedirectToPage("Login");
            }

            var cartJson = HttpContext.Session.GetString("cart");
			cartItems = JsonSerializer.Deserialize<List<CartItem>>(cartJson).OrderBy(x => x.ProductId).ToList();

			return Page();
        }
    }
}
