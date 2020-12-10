using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Areas.Identity.Pages.Admin.Products
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
        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            var products = from p in _context.Product select p;
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(p => p.Name.ToLower().Contains(SearchString.ToLower()));
            }

            Product = await products
                .Include(p => p.Category).ToListAsync();
        }
    }
}
