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


        public List<Customer> listc;
        public IndexModel(PRN221DBContext dbcontext)
        {
            _context = dbcontext;
            listc= _context.Customers.ToList();


        }
        [BindProperty]
        public int Page { get; set; }
        [BindProperty]
        public int NumberPage { get; set; }
        public Account acc { get; set; }
        
        [BindProperty]
        public string StartDate { get; set; }
        [BindProperty]
        public string EndDate { get; set; }

        [BindProperty]
        public List<Order> orders { get; set; }
        [BindProperty]
        public List<OrderDetail> orderdetail { get; set; }
        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            string data = HttpContext.Session.GetString("Account");
            if (data != null)
            {
                Account acount = JsonSerializer.Deserialize<Account>(data);
                if (acount != null)
                {
                    if (acount.CustomerId != null)
                    {

                        acc = acount;
                        var oderdetails = await _context.OrderDetails.Include(x => x.Order).Include(x => x.Product).
                            Where(x => x.Order.CustomerId == acount.CustomerId).
                            OrderByDescending(x => x.Order.OrderDate).ToListAsync();
                       
                        var orderSort = await _context.Orders
                       .Where(x => x.CustomerId == acount.CustomerId)
                       .OrderByDescending(x => x.OrderDate)
                        .ToListAsync();
                      //  orders = orderSort;
                        orderdetail = oderdetails;

                        int unitpageSize = 6;
                        pageIndex = pageIndex ?? 1;
                        Page = pageIndex.Value;
                        int total = orderSort.Count;
                        NumberPage = (int)Math.Ceiling((double)total / (double)unitpageSize);

                        orders = await _context.Orders
                       .Where(x => x.CustomerId == acount.CustomerId)
                       .OrderByDescending(x => x.OrderDate).Skip((pageIndex.Value - 1) * unitpageSize).Take(unitpageSize)
                        .ToListAsync();

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
        public async Task<IActionResult> OnPost(string id)
        {
            string data = HttpContext.Session.GetString("Account");
            if (data != null)
            {
                Account acount = JsonSerializer.Deserialize<Account>(data);
                if (acount != null)
                {
                    if (acount.CustomerId != null)
                    {
                        acc = acount;
                       

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
