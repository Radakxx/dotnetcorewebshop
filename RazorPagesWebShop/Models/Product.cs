using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesWebShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        [Display(Name = "About this product:")]
        public string Description { get; set; }
        [Display(Name = "Disable purchases")]
        public bool IsDisabled { get; set; }
        public bool IsFeatured { get; set; }
        public int Stock { get; set; }
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductToCart> ProductsToCarts { get; set; } = new List<ProductToCart>();
        public ICollection<ProductToTag> ProductsToTags { get; set; } = new List<ProductToTag>();
        public ICollection<ProductToOrder> ProductsToOrders { get; set; } = new List<ProductToOrder>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public double AverageOfRatings()
        {
            double count = 0;
            double sum = 0;
            foreach (var rating in Ratings)
            {
                sum += (int)rating.RatingValue;
                ++count;
            }
            return sum / count;
        }
    }
}