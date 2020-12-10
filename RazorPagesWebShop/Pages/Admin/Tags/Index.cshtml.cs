using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages.Admin.Tags
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
        public IList<Tag> Tag { get;set; }

        public async Task OnGetAsync()
        {
            var tags = from t in _context.Tag select t;
            if (!string.IsNullOrEmpty(SearchString))
            {
                tags = tags.Where(t => t.Name.ToLower().Contains(SearchString.ToLower()));
            }
            Tag = await tags.ToListAsync();
        }
    }
}
