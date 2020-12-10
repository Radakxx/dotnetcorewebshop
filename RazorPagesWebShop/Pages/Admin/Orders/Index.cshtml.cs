using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;

        public IndexModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            var orders = from o in _context.Order select o;
            if (!string.IsNullOrEmpty(SearchString))
            {
                orders = orders.Where(o => o.UserId.Equals(SearchString));
            }
            Order = await orders
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
