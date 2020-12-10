using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages.Admin.Categories
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
        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            var categories = from c in _context.Category select c;
            if (!string.IsNullOrEmpty(SearchString))
            {
                categories = categories.Where(c => c.Name.ToLower().Equals(SearchString.ToLower()));
            }
            Category = await categories.ToListAsync();
        }
    }
}
