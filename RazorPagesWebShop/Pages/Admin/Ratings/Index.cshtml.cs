using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages.Admin
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
        public IList<Rating> Rating { get;set; }

        public async Task OnGetAsync()
        {
            var ratings = from r in _context.Rating select r;
            if (!string.IsNullOrEmpty(SearchString))
            {
                ratings = ratings.Where(s => s.Product.Name.ToLower().Contains(SearchString.ToLower()));
            }
            Rating = await ratings
                .Include(r => r.Product)
                .Include(r => r.User)
                .ToListAsync();
        }
    }
}
