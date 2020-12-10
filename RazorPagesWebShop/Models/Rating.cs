using System.ComponentModel.DataAnnotations;

namespace RazorPagesWebShop.Models
{
    public enum RatingValue
    {
        Bad,
        Poor,
        Average,
        Great,
        Excellent
    }

    public class Rating
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Rating")]
        public RatingValue RatingValue { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}