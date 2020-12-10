using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesWebShop.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Areas.Identity.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;

        public CreateModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile FormFile { get; set; }

        public async Task<IActionResult> OnPostAsync([FromServices] IHostingEnvironment hostingEnvironment)
        {
          
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (FormFile != null && FormFile.Length > 0)
            {
                var webRoot = hostingEnvironment.WebRootPath;
                Product.Image = $"image-{DateTime.UtcNow.ToString("yyyyMMdd-HHmmss")}.jpg"; // wwwroot/images könyvtárba fogja menteni.
                
                var filePath = Path.Combine(webRoot, "images", Product.Image);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await FormFile.CopyToAsync(stream);
                }
            }

            await _context.Product.AddAsync(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
