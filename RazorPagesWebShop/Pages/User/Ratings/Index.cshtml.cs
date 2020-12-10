using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages.Ratings
{
    [Authorize(Roles = "Admin, User")]
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;

        public IndexModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }

        public IList<Rating> Rating { get;set; }
        public async Task OnGetAsync()
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Rating = await _context.Rating
                .Where(r => r.User.Id == currentUserName)
                .Include(r => r.Product)
                .Include(r => r.User).ToListAsync();
        }
    }
}
