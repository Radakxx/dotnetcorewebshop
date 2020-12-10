using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages.Admin.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;

        public DetailsModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }
        public IList<ProductToOrder> ProductToOrder { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Order
                .Include(o => o.User)
                .Include(o=>o.ProductsToOrders).ThenInclude(pto=>pto.Product)
                .Include(o =>o.OrderInfo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Order == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
