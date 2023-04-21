using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject_Team2.Models;
using System.Text.Json;

namespace PRN221_FinalProject_Team2.Pages.Member.Orders
{
    public class IndexModel : PageModel
    {
        private readonly PRN221DBContext _context;
       

        public IndexModel(PRN221DBContext dbcontext)
        {
            _context = dbcontext;         
            
        }
        public List<Order> orders { get; set; }
        public List<OrderDetail> orderdetail { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            string data = HttpContext.Session.GetString("userdata");
            if (data != null)
            {
                Account acount = JsonSerializer.Deserialize<Account>(data);
                if (acount != null)
                {
                    if (acount.CustomerId != null)
                    {
                        var oderdetails = await _context.OrderDetails.Include(x => x.Order).Include(x => x.Product).
                            Where(x => x.Order.CustomerId == acount.CustomerId).
                            OrderByDescending(x => x.Order.OrderDate).ToListAsync();
                        var orderSort = await _context.Orders
                       .Where(x => x.CustomerId == acount.CustomerId)
                       .OrderByDescending(x => x.OrderDate)
                        .ToListAsync();
                        orders = orderSort;
                        orderdetail = oderdetails;

                        return Page();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                
                


            }

            return NotFound();




        }
    }

}
