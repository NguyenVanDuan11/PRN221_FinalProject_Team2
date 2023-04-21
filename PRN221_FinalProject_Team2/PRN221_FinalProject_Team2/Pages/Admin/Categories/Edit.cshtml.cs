using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221_FinalProject_Team2.Models;

namespace PRN221_FinalProject_Team2.Pages.Admin.Categories
{
	[BindProperties]
	public class EditModel : PageModel
	{
		private readonly PRN221DBContext _db;

		public EditModel(PRN221DBContext db)
		{
			_db = db;
		}

		public Category Category { get; set; }

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Category = await _db.Categories.FindAsync(id);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			TempData["Success"] = "Update successfully!";
			_db.Categories.Update(Category);
			await _db.SaveChangesAsync();
			return RedirectToPage("Index");
		}
	}
}
