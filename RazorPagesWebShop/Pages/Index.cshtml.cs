using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RazorPagesWebShop.Data;
using RazorPagesWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly RazorPagesWebShopContext _context;

        public IndexModel(ILogger<IndexModel> logger, RazorPagesWebShopContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IList<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            var products = from p in _context.Product select p;
            Products = await products
                .Where(p => p.IsFeatured) // kikeressük a kiemelt termékeket
                .ToListAsync();
        }
    }
}
