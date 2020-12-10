using System.Collections.Generic;

namespace RazorPagesWebShop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<ProductToCart> ProductsToCarts { get; set; } = new List<ProductToCart>();
        public decimal TotalPrice()
        {
            decimal ret = 0;
            foreach (var p in ProductsToCarts)
            {
                ret += p.ProductQuantity * p.Product.Price;
            }
            return ret;
        }
    }
}