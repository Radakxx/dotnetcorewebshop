namespace RazorPagesWebShop.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string HouseNumber { get; set; }
        public string Phone { get; set; }
    }
}
