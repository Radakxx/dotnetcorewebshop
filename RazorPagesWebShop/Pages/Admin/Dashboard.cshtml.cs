using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesWebShop.Data;

namespace RazorPagesWebShop.Areas.Identity.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class DashboardModel : PageModel
    {
        private readonly RazorPagesWebShopContext _context;
        public DashboardModel(RazorPagesWebShop.Data.RazorPagesWebShopContext context) 
        {
            _context = context;
        }
        public void OnGet()
        {
         
        }
    }
}