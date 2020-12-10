using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RazorPagesWebShop.Pages
{
    [Authorize(Roles = "Admin, User")]
    public class CartModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;
        public CartModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }
        public IList<Product> Product { get; set; }
        public Cart Cart { get; set; }
        public IList<ProductToCart> ProductsToCarts { get; set; }

        public async Task OnGetAsync()
        {
            var _user = await _context.Users.Where(u => u.Id == (User.FindFirst(ClaimTypes.NameIdentifier).Value).ToString())
                .Include(u => u.Cart).FirstOrDefaultAsync(); // ClaimTypes segítségével elkérjük a felhasználót és a kosarát az adatbázistól

            if (_user.Cart == null) // Ha nincs aktív kosara, létrehozunk neki egy üres kosarat
            {
                Cart c = new Cart { UserId = _user.Id };
                _context.Cart.Add(c);
                await _context.SaveChangesAsync();
            }

            Cart = await _context.Cart // Elkérjük a kosárhoz tartozó termékeket
                .Where(c => c.User.Id == _user.Id)
                .Include(pc => pc.ProductsToCarts)
                .ThenInclude(p => p.Product).FirstOrDefaultAsync();

            if(Cart.ProductsToCarts != null) // Ha vannak termék-kosár kapcsolatok, azokatt is kigyűjtjük
            {
                ProductsToCarts = Cart.ProductsToCarts.ToList();
            }
        }

        public async Task<RedirectToPageResult> OnGetMinus(int productid, int cartid) 
        {
            var productToCart = _context.ProductToCart.Include(p=>p.Product).FirstOrDefault(p => p.ProductId == productid && p.CartId == cartid); // Kikeressük a termék-kosár kapcsolatot
            productToCart.ProductQuantity -= 1; // Csökkentjük a mennyiséget
            if (productToCart.ProductQuantity < 1) // Ha 1 alá csökkent a mennyiség, kitöröljük a kosárból a terméket
            {
                var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Cart = await _context.Cart.Where(c => c.User.Id == currentUserName).Include(pc => pc.ProductsToCarts).FirstOrDefaultAsync();
                Cart.ProductsToCarts.Remove(productToCart);
            }
            await _context.SaveChangesAsync(); // Mentjük a változtatásokat
            return RedirectToPage(); // Visszatérünk az oldalra
        } 
        public async Task<RedirectToPageResult> OnGetPlus(int productid, int cartid) 
        {
            var productToCart = _context.ProductToCart.FirstOrDefault(p => p.ProductId == productid && p.CartId == cartid); // Kikeressük a termék-kosár kapcsolatot
            productToCart.ProductQuantity += 1; // Növeljük a mennyiséget
            await _context.SaveChangesAsync(); // Mentjük a változtatásokat
            return RedirectToPage(); // Visszatérünk az oldalra
        }

        public async Task<RedirectToPageResult> OnPostAsync(int? id)
        {
            var currentUserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Cart = await _context.Cart.Where(c => c.User.Id == currentUserName).Include(pc => pc.ProductsToCarts).ThenInclude(p=>p.Product).FirstOrDefaultAsync();
            if (id != null) // Ha van id, konkrét terméket kell eltávolítani
            {
                var c = Cart.ProductsToCarts.FirstOrDefault(p => p.ProductId == id); // Kikeressük azt a termék-kosár kapcsolatot, amiben a megadott termék szerepel
                Cart.ProductsToCarts.Remove(c); // Eltávolítjuk a kosárból
            }
            else // Egyébként a teljes kosár tartalmát töröljük
            {
                Cart.ProductsToCarts.Clear();
            }
            await _context.SaveChangesAsync(); // Mentjük a változtatásokat
            return RedirectToPage(); // Visszatérünk az oldalra
        }
    }
}