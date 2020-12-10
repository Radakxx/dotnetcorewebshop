using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Models;

namespace RazorPagesWebShop.Data
{
    public class RazorPagesWebShopContext : IdentityDbContext<User>
    {
        public RazorPagesWebShopContext (DbContextOptions<RazorPagesWebShopContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesWebShop.Models.Product> Product { get; set; }
        public DbSet<RazorPagesWebShop.Models.Cart> Cart { get; set; }
        public DbSet<RazorPagesWebShop.Models.Category> Category { get; set; }
        public DbSet<RazorPagesWebShop.Models.Tag> Tag { get; set; }
        public DbSet<RazorPagesWebShop.Models.ProductToTag> ProductToTag { get; set; }
        public DbSet<RazorPagesWebShop.Models.ProductToCart> ProductToCart { get; set; }
        public DbSet<RazorPagesWebShop.Models.Order> Order { get; set; }
        public DbSet<RazorPagesWebShop.Models.OrderInfo> OrderInfo { get; set; }
        public DbSet<RazorPagesWebShop.Models.Rating> Rating { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().ToTable("Product");
            builder.Entity<Category>().ToTable("Category");
            builder.Entity<Tag>().ToTable("Tag");
            builder.Entity<ProductToTag>().ToTable("ProductToTag");
            builder.Entity<ProductToTag>().HasKey(pt => new { ProductID = pt.ProductId, TagID = pt.TagId });
            builder.Entity<ProductToCart>().ToTable("ProductToCart");
            builder.Entity<ProductToCart>().HasKey(pc => new { ProductID = pc.ProductId, CartID = pc.CartId });
            builder.Entity<Cart>().ToTable("Cart");
            builder.Entity<User>().HasOne(u => u.Cart).WithOne(c => c.User).HasForeignKey<Cart>(u => u.UserId);
        }
    }
}
