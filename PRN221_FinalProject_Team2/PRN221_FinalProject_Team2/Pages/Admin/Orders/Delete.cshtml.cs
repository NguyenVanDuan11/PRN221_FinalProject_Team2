using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_FinalProject_Team2.Models;

namespace PRN221_FinalProject_Team2.Pages.Admin.Orders
{
    public class DeleteModel : PageModel
    {

        private readonly PRN221_FinalProject_Team2.Models.PRN221DBContext _db;

        public DeleteModel(PRN221_FinalProject_Team2.Models.PRN221DBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _db.Orders == null)
            {
                return NotFound();
            }
            IList<Customer> customerList = await _db.Customers.ToListAsync();
            var order = await _db.Orders.FirstOrDefaultAsync(m => m.OrderId == id);


            if (order == null)
            {
                return NotFound();
            }
            else
            {
                for (int j = 0; j < customerList.Count; j++)
                {
                    if (order.CustomerId == customerList[j].CustomerId)
                    {
                        order.Customer = customerList[j];
                    }
                }
                Order = order;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _db.Orders == null)
            {
                return NotFound();
            }
            var order = await _db.Orders.FindAsync(id);

            if (order != null)
            
                Order = order;
            IList<OrderDetail> OrderDetailList = await _db.OrderDetails.ToListAsync();
            for(int i=0;i< OrderDetailList.Count; i++)
            {
                if (Order.OrderId.Equals(OrderDetailList[i].OrderId)){
                    _db.OrderDetails.Remove(OrderDetailList[i]);
                    await _db.SaveChangesAsync();

                }
            }
            _db.Orders.Remove(Order);
                await _db.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
