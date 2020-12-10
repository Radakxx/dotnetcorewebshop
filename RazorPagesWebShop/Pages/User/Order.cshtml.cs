using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;

namespace RazorPagesWebShop.Pages
{
    public class OrderModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;
        public OrderModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        public Cart Cart { get; set; }
        [BindProperty]
        public OrderInfo OrderInfo { get; set; }
     
        public async Task OnGetAsync()
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Cart = await _context.Cart.Where(c => c.User.Id == currentUserName).Include(pc => pc.ProductsToCarts).ThenInclude(p => p.Product).FirstOrDefaultAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value; // ClaimTypes segítségével elkérjük a UserId-t
            Cart = await _context.Cart.Where(c => c.User.Id == currentUserName)
                .Include(pc => pc.ProductsToCarts).ThenInclude(p => p.Product).FirstOrDefaultAsync(); // Elkérjük a kosarát és a benne lévő termékeket az adatbáziskontextustól
            
            ICollection<ProductToOrder> pto = new List<ProductToOrder>();
            Order o = new Order() { UserId = currentUserName, Status = 0, ProductsToOrders = pto, OrderDate = DateTime.Now, OrderInfo = OrderInfo }; // Létrehozzuk a rendeléshez szükséges objektumokat

            foreach (ProductToCart ptc in Cart.ProductsToCarts) // Minden kosár-termék kapcsolatot megrendelés-termék kapcsolatra képezünk le
            {
                ptc.Product.Stock -= ptc.ProductQuantity; // Mindeközben levonjuk a raktárkészletből a megrendelt termékeket
                o.ProductsToOrders.Add(new ProductToOrder() { OrderId = o.Id, ProductId = ptc.ProductId, ProductQuantity = ptc.ProductQuantity }); 
            }
            _context.OrderInfo.Add(OrderInfo);
            _context.Order.Add(o);
            _context.Remove(Cart); // A megrendelés végeztével a felhasználó kosarát kitöröljük
            await _context.SaveChangesAsync(); // Mentjük a változtatásokat
            return RedirectToPage("/User/Products/Index"); // Visszatérünk a főoldalra
        }
    }
}
