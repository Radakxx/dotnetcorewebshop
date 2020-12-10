using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RazorPagesWebShop.Models
{
    public class User : IdentityUser
    {
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}