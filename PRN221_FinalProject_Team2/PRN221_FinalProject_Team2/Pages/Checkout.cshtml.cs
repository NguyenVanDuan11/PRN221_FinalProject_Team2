using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_FinalProject_Team2.Models;
using System.Text.Json;

namespace PRN221_FinalProject_Team2.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly PRN221DBContext _dbContext;
        [BindProperty]
        public Order order { get; set; } = new Order();

        [BindProperty]
        public decimal? totalPrice { get; set; } = 0;

        public CheckoutModel(PRN221DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGet(decimal? totalPrice)
        {
            

            var userData = HttpContext.Session.GetString("Account");
            if (userData != null)
            {
                var cartItems = HttpContext.Session.GetString("cart");
               
                if (string.IsNullOrEmpty(cartItems))
                {
                    return RedirectToPage("/Error");
                }
                else
                {
					this.totalPrice = totalPrice;
					return Page();
				}
				
            }
            else
            {
				return RedirectToPage("/Error");
			}


           


        }
        public async Task<IActionResult> OnPost()
        {
            string cart = HttpContext.Session.GetString("cart");
            var userData = HttpContext.Session.GetString("Account");
            Account acc = JsonSerializer.Deserialize<Account>(userData);
            List<CartItem> cartItems = JsonSerializer.Deserialize<List<CartItem>>(cart);
            if (order != null)
            {

                order.CustomerId = acc.CustomerId;
                order.OrderDate = DateTime.Now;
                order.OrderId = 0;
                _dbContext.Orders.Add(order);
                int i = 0;
            }
            try
            {
               int i = _dbContext.SaveChanges();
                if (i > 0)
                {
                    int id = _dbContext.Orders.OrderByDescending(x=>x.OrderId).FirstOrDefault().OrderId;
                    List<OrderDetail> orderDetails = new List<OrderDetail>();
                    foreach (var c in cartItems)
                    {
                        orderDetails.Add(new OrderDetail { OrderId = id, ProductId = c.ProductId.Value, Quantity = (short)c.Quantity.Value, UnitPrice = c.UnitPrice.Value });
                    }
                    int a = 0;
                   _dbContext.OrderDetails.AddRange(orderDetails);
                    a=  _dbContext.SaveChanges();
                    if (a > 0)
                    {
                        foreach (var o in orderDetails)
                        {
                            _dbContext.Products.Where(x => x.ProductId == o.ProductId).FirstOrDefault().UnitsInStock -= o.Quantity;
                            a++;
                        }
                        _dbContext.SaveChanges();

                    }
                   
                    else
                    {

                    }
					return RedirectToPage("/Member/Orders/Index");

				}
				else
                {
                    return Page();
                }

            }
            catch
            {
                return Page();
            }

        }
    }

}
