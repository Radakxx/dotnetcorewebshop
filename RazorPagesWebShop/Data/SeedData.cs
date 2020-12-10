using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RazorPagesWebShop.Data;
using System;
using System.Linq;

namespace RazorPagesWebShop.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, RazorPagesWebShopContext context,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (context.Product.Any())  
            { 
                return; // DB has been seeded
            }
            InitializeRoles(roleManager); 
            InitializeUsers(userManager);
            InitializeData(context);
        }

        public static void InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        static void InitializeUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("1@gmail.com").Result == null)
            {
                User user = new User();
                user.UserName = "First";
                user.Email = "1@gmail.com";
                user.EmailConfirmed = true;
                user.FirstMidName = "Radványi";
                user.LastName = "Álmos";
                IdentityResult result = userManager.CreateAsync(user, "Proba123'").Result;
                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "Admin").Wait();
            }

            if (userManager.FindByNameAsync("2@gmail.com").Result == null)
            {
                User user = new User();
                user.UserName = "2@gmail.com";
                user.Email = "2@gmail.com";
                user.EmailConfirmed = true;
                user.FirstMidName = "Végh";
                user.LastName = "Béla";
                IdentityResult result = userManager.CreateAsync(user, "Proba123'").Result;
                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "User").Wait();
            }

            if (userManager.FindByNameAsync("3@gmail.com").Result == null)
            {
                User user = new User();
                user.UserName = "Third";
                user.Email = "3@gmail.com";
                user.EmailConfirmed = true;
                user.FirstMidName = "Lakatos";
                user.LastName = "Kurvaanyád";
                IdentityResult result = userManager.CreateAsync(user, "Proba123'").Result;
                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "User").Wait();
            }
        }
        static void InitializeData(RazorPagesWebShopContext context) 
        {
            var categories = new Category[]
               {
                    new Category { Name = "Electronic", Description = "Electronic description" },
                    new Category { Name = "Useless", Description = "Useless description" },
                    new Category { Name = "Entertainment", Description = "Entertainment description" },
                    new Category { Name = "Fitness", Description = "Fitness description" },
                    new Category { Name = "Sport", Description = "Sport description" },
                    new Category { Name = "Movies", Description = "Movies description" }
               };
            foreach (Category category in categories) { context.Category.Add(category); }
            context.SaveChanges();

            var products = new Product[]
                {
                    new Product { Name = "Apple iPhone 15XS mobiltelefon", Description = "Revolutionary...", Price = 7.99M, Image ="apple-iphone-15xs.jpg",
                        Category = categories.Single(c => c.Name == "Electronic") },
                    new Product { Name = "Apple Mac Pro kerékkészlet", Description = "MKBHD explains", Price = 279.990M, Image ="apple-mac-pro-wheels.jpg",
                        Category = categories.Single(c => c.Name == "Useless") },
                    new Product { Name = "Apple MacBook Air 13' 2016", Description = "A  képernyő katasztrofális, de a trackpad zseniális", Price = 320.990M, Image ="apple-macbook-air.jpeg",
                        Category = categories.Single(c => c.Name == "Electronic") },
                    new Product { Name = "Sennheiser Game One fejhallgató", Description = "A legjobb fejhallgató", Price = 53.990M, Image ="sennheiser-game-one.jpg",
                        Category = categories.Single(c => c.Name == "Electronic") },
                    new Product { Name = "DJI Mavic Air 2 drón", Description = "xDD drón goes zümm zümm", Price = 299.990M, Image ="dji-mavic-air-2.png",
                        Category = categories.Single(c => c.Name == "Electronic") },
                    new Product { Name = "Apple iPhone SE (2020) 64GB mobiltelefon", Description = "Újrahasznosítjuk", Price = 176.990M, Image ="apple-iphone-se-2020.jpg",
                        Category = categories.Single(c => c.Name == "Electronic") },
                    new Product { Name = "Scitec Nutrition Vitamin D3 Forte", Description = "Véd a Covid ellen Kappa", Price = 3.030M, Image ="scitec-nutrition-d3-vitamin-forte.jpg",
                        Category = categories.Single(c => c.Name == "Fitness") },
                    new Product { Name = "Generikus C Vitamin", Description = "A skorbutig jó, utána haszontalan", Price = 2.990M, Image ="generic-c-vitamin.jpg",
                        Category = categories.Single(c => c.Name == "Useless") },
                    new Product { Name = "Egy rendesen összerakott Gamer PC", Description = "Nem spórolunk a tápon", Price = 357.990M, Image ="correct-gamer-pc.png",
                        Category = categories.Single(c => c.Name == "Electronic") },
                    new Product { Name = "Frédi és Béni teljes DVD gyűjtemény", Description = "Rímhányó", Price = 107.990M, Image ="flintstones.jpg",
                        Category = categories.Single(c => c.Name == "Movies") },
                    new Product { Name = "Lego Star Wars Slave I", Description = "Boba Fett? Hol van?", Price = 44.590M, Image ="lego-star-wars-slave-i.jpg",
                        Category = categories.Single(c => c.Name == "Entertainment") },
                    new Product { Name = "Szellemirtók (Ghostbusters DVD)", Description = "It's actually a korean cam version, but who you gonna call?", Price = 4.990M, Image ="ghostbusters.jpg",
                        Category = categories.Single(c => c.Name == "Movies") },
                    new Product { Name = "Scitec Whey Protein Professional 100% 5kg jegeskávé íz", Description = "Egy álom...", Price = 27.990M, Image ="scitec-wpp.png",
                        Category = categories.Single(c => c.Name == "Fitness") },
                    new Product { Name = "A lehető LEGROSSZABB Gamer PC", Description = "From india with love", Price = 335.990M, Image ="bad-gamer-pc.jpeg",
                        Category = categories.Single(c => c.Name == "Electronic") },
                    new Product { Name = "Lego Star Wars X-szárnyú", Description = "Vettem vörös ötös", Price = 34.990M, Image ="lego-star-wars-x-wing.jpg",
                        Category = categories.Single(c => c.Name == "Entertainment") },
                    new Product { Name = "Lego Architecture Szabadság-szobor", Description = "Majmok bolygója", Price = 27.490M, Image ="lego-architecture-statue-of-liberty.jpg",
                        Category = categories.Single(c => c.Name == "Entertainment") },
                    new Product { Name = "Merida Dual Thrust bicikli", Description = "Nem vagyok orgazda", Price = 3.990M, Image ="merida-dual-thrust.jpg",
                        Category = categories.Single(c => c.Name == "Sport") },
                    new Product { Name = "Schönherzes Roller", Description = "Papucsban az igazi", Price = 7.990M, Image ="roller.jpg",
                        Category = categories.Single(c => c.Name == "Sport") },
                    new Product { Name = "Dino Bikes Szívecskés kerékpár 14-es méretben", Description = "Ne kérdezd", Price = 44.990M, Image ="dino-bikes-szivecskes.jpg",
                        Category = categories.Single(c => c.Name == "Sport") },
                    new Product { Name = "Dino Bikes Amerika Kapitány kerékpár 12-es méretben", Description = "Anya néfd fog nélkül", Price = 44.990M, Image ="dino-bikes-amerika-kapitany.jpg",
                        Category = categories.Single(c => c.Name == "Sport") }
                };
            foreach (Product product in products)
            {
                context.Product.Add(product);
            }
            context.SaveChanges();
             var tags = new Tag[]
                           {
                                new Tag { Name = "Apple", Description = "The most revolutionary brand" },
                                new Tag { Name = "Whey Protein", Description = "Steroids" },
                                new Tag { Name = "Electronic", Description = "Anything your grandma doesn't understand" },
                                new Tag { Name = "PC", Description = "> mac" }
                           };
            
            foreach (Tag tag in tags) { context.Tag.Add(tag); }
            context.SaveChanges();

            var _product = context.Product.FirstOrDefault(a => a.Name == "Apple iPhone 15XS mobiltelefon");
            var _tag = context.Tag.FirstOrDefault(b => b.Name == "Apple");
            var _producttag = context.ProductToTag.Include(a => a.Product).Include(b => b.Tag).FirstOrDefault(c => c.Product.Name == "Apple iPhone 15XS mobiltelefon" && c.Tag.Name == "Apple");
            if (_producttag == null) { context.ProductToTag.Add(new ProductToTag { ProductId = _product.Id, TagId = _tag.Id }); }  context.SaveChanges();

            _product = context.Product.FirstOrDefault(a => a.Name == "Scitec Whey Protein Professional 100% 5kg jegeskávé íz");
            _tag = context.Tag.FirstOrDefault(b => b.Name == "Whey Protein");
            _producttag = context.ProductToTag.Include(a => a.Product).Include(b => b.Tag).FirstOrDefault(c => c.Product.Name == "Scitec Whey Protein Professional 100% 5kg jegeskávé íz" && c.Tag.Name == "Whey Protein");
            if (_producttag == null) { context.ProductToTag.Add(new ProductToTag { ProductId = _product.Id, TagId = _tag.Id }); }
            context.SaveChanges();

            _product = context.Product.FirstOrDefault(a => a.Name == "Apple iPhone 15XS mobiltelefon");
            _tag = context.Tag.FirstOrDefault(b => b.Name == "Electronic");
            _producttag = context.ProductToTag.Include(a => a.Product).Include(b => b.Tag).FirstOrDefault(c => c.Product.Name == "Apple iPhone 15XS mobiltelefon" && c.Tag.Name == "Electronic");
            if (_producttag == null) { context.ProductToTag.Add(new ProductToTag { ProductId = _product.Id, TagId = _tag.Id }); }
            context.SaveChanges();

            _product = context.Product.FirstOrDefault(a => a.Name == "A lehető LEGROSSZABB Gamer PC");
            _tag = context.Tag.FirstOrDefault(b => b.Name == "PC");
            _producttag = context.ProductToTag.Include(a => a.Product).Include(b => b.Tag).FirstOrDefault(c => c.Product.Name == "A lehető LEGROSSZABB Gamer PC" && c.Tag.Name == "PC");
            if (_producttag == null) { context.ProductToTag.Add(new ProductToTag { ProductId = _product.Id, TagId = _tag.Id }); }
            context.SaveChanges();
        }
    }
}
