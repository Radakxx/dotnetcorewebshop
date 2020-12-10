using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesWebShop.Models
{
    public enum Status
    {
        Processing,
        Processed,
        Shipping,
        Completed,
        Failed
    }

    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Status Status { get; set; }
        public ICollection<ProductToOrder> ProductsToOrders { get; set; } = new List<ProductToOrder>();
        public OrderInfo OrderInfo { get; set; }
        [Display(Name = "Date")]
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice()
        {
            decimal ret = 0;
            foreach(var p in ProductsToOrders)
            {
                ret += p.ProductQuantity * p.Product.Price;
            }
            return ret;
        }
    }
}