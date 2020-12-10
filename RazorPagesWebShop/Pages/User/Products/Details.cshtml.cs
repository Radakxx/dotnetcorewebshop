using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesWebShop
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;
        private readonly UserManager<User> _userManager;

        public DetailsModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public Rating Rating { get; set; }
        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) { return NotFound(); }
            Product = await _context.Product
                .Include(c => c.Category)
                .Include(c => c.Ratings).ThenInclude(r => r.User)
                .Include(c => c.ProductsToTags ).ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Product == null) { return NotFound(); }
            return Page();
        }

        public async Task<IActionResult> OnPostRatingAsync(int? id)
        {
            if (id == null) { return NotFound(); }
            if (!ModelState.IsValid) { return Page(); }
            
            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (Product == null) { return NotFound(); }

            var _user = _context.Users.Where(u => u.Id == (_userManager.GetUserId(User)).ToString()).Include(u => u.Ratings).FirstOrDefault();
            _context.Rating.Add( // Új értékelés hozzáadása
                new Rating { Title = Rating.Title, Description = Rating.Description, RatingValue = Rating.RatingValue, User = _user, Product = Product, ProductId = Product.Id });
            await _context.SaveChangesAsync(); // Változtatások mentése
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCartAsync(int? id)
        {
            if (id == null) { return NotFound(); }
            if (!ModelState.IsValid) { return Page(); }
            
            Product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (Product == null) { return NotFound(); }

            if(Quantity == 0) { Quantity = 1; }

            var _userId = _userManager.GetUserId(User);
            var _user = _context.Users
                .Where(u => u.Id == _userId.ToString())
                .Include(u => u.Cart)
                .ThenInclude(c => c.ProductsToCarts)
                .ThenInclude(p => p.Product)
                .FirstOrDefault();
            if (_user.Cart == null) // Ha nincs aktív kosara
            {
                Cart c = new Cart { UserId = _user.Id }; // Új kosár létrehozása
                c.ProductsToCarts.Add(new ProductToCart {Id=0, CartId = c.Id, ProductId = Product.Id, ProductQuantity = Quantity}); // A választott termék kosárhoz adása
                _context.Cart.Add(c); 
            }
            else // Ha van aktív kosara
            {
                Cart c = _user.Cart;
                var ptc = c.ProductsToCarts.FirstOrDefault(p => p.ProductId == Product.Id); // Van-e már a kosarában ebből a termékből
                if (ptc == null) // Ha nincs, hozzáadjuk
                {
                    c.ProductsToCarts.Add(new ProductToCart {Id =0, CartId = c.Id, ProductId = Product.Id, ProductQuantity = Quantity }); 
                }
                else {ptc.ProductQuantity += Quantity; } // Különben egyel növeljük a mennyiséget 
            }
            await _context.SaveChangesAsync(); // Változtatások mentése
            return RedirectToPage("/User/Cart"); // Átirányítás a kosárra
        }
    }
}
