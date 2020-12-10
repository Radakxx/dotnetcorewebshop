using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Areas.Identity.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;

        public EditModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public List<int> SelectedTags { get; set; }
        [BindProperty]
        public IFormFile UploadImage { get; set; }
        public List<SelectListItem> TagsOptions { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.ProductsToTags)
                .ThenInclude(pt => pt.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            TagsOptions = await _context.Tag.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToListAsync();

            if (Product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Tags = await _context.Tag.ToListAsync();

            if(SelectedTags.Count > 0)
            {
                _context.ProductToTag.RemoveRange(_context.ProductToTag.Where(p => p.ProductId.Equals(Product.Id)).ToList());
                await _context.SaveChangesAsync();
            }
                   
            foreach (var t in SelectedTags)
            {
                Product.ProductsToTags.Add(new ProductToTag { Product = Product,Tag = Tags.Where(tag => tag.Id.Equals(t)).FirstOrDefault() });
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
