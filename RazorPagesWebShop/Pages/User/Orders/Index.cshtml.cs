using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages.Orders
{
    [Authorize(Roles = "Admin, User")]
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;

        public IndexModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Order = await _context.Order
                .OrderByDescending(d => d.OrderDate) // A felhasználónak a megrendeléseket a megrendelés ideje szerinti fordított sorrendben listázzuk, így mindig a legutóbbi megrendelés van legfelül
                .Where(c => c.User.Id == currentUserName)
                .Include(o => o.User)
                .Include(pto => pto.ProductsToOrders).ThenInclude(p => p.Product)
                .ToListAsync();
        }
    }
}
