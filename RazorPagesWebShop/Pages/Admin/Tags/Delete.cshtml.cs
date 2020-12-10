using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages.Admin.Tags
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;

        public DeleteModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tag Tag { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tag = await _context.Tag.FirstOrDefaultAsync(m => m.Id == id);

            if (Tag == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tag = await _context.Tag.FindAsync(id);

            if (Tag != null)
            {
                _context.Tag.Remove(Tag);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
