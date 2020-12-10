using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;

        public DetailsModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }

        public Rating Rating { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rating = await _context.Rating
                .Include(r => r.Product)
                .Include(r => r.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Rating == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
