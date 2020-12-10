namespace RazorPagesWebShop.Models
{
    public class ProductToOrder
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int ProductQuantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}