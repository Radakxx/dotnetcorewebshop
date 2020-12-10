﻿namespace RazorPagesWebShop.Models
{
    public class ProductToCart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int ProductQuantity { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}