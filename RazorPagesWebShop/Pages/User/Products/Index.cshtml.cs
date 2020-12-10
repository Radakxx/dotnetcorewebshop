using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesWebShop
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesWebShop.Data.RazorPagesWebShopContext _context;
        public IndexModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context)
        {
            _context = context;
        }
        public IList<Product> Products { get;set; }
        public SelectList Categories { get; set; }
        public List<SelectListItem> TagsOptions { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<int> SelectedTags { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ProductCategory { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, string category, List<int> tags)
        {
            ViewData["CurrentSort"] = sortOrder; // A termékek rendezésének jelenlegi módja
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // A név szerinti rendezés aktuális módja
            ViewData["PriceSortParam"] = sortOrder == "price_asc" ? "price_desc" : "price_asc"; // Az ár szerinti rendezés aktuális módja

            if(searchString == null) { searchString = currentFilter; } 
            ViewData["CurrentFilter"] = searchString;
            if (category == null) { category = ""; }
            ViewData["Category"] = category;

            var categoryQuery = from p in _context.Product orderby p.Category select p.Category.Name; // LINQ segítségével lekérjük a kategóriák neveinek listáját
            var products = from p in _context.Product select p; // A termékek listáját

            if (!string.IsNullOrEmpty(searchString)) { products = products.Where(s => s.Name.ToLower().Contains(searchString.ToLower())); }
            if (!string.IsNullOrEmpty(category)) { products = products.Where(x => x.Category.Name == category); }

            if(SelectedTags != null)
            {
                foreach (var t in tags)
                {
                    products = products.Where(x => x.ProductsToTags.Any(pt=>pt.Tag.Id == t));        
                }
            }
            
        
            switch (sortOrder) // A rendezési mód alapján rendezzük sorba a termékek listáját
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()); 
            TagsOptions = await _context.Tag.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToListAsync();
            Products = await products.ToListAsync(); // Kiértékeljük a lekérést
        }
    }
}
