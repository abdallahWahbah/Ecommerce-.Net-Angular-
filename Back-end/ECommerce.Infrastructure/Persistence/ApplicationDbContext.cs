
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        { 
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Admin",
                    Email = "admin@admin.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEKcP55h+md51rw3sJ+3hPPxBkLYsA4lPDj+MUvfSJ9N8mOZgcX/7SgJhZO2ZNxWJ4g==", // admin123
                    Role = "Admin"
                },
                new User
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Buyer",
                    Email = "buyer@buyer.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEDPStHtHtQ8nWRVwPLtnHMIOX8Cz+lVeQhYKpxDiLbwjs7CKVS3zZmTdEIVHr4/nBA==", // buyer123
                    Role = "Buyer"
                }
            );

            // Categories
            var electronicsId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var clothesId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var booksId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = electronicsId,
                    Name = "Electronics"
                },
                new Category
                {
                    Id = clothesId,
                    Name = "Clothes"
                },
                new Category
                {
                    Id = booksId,
                    Name = "Books"
                }
            );

            // Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000101"), Name = "Laptop", Description = "Keyboard for beginners", Price = 25, StockQuantity = 100, ImageUrl = "/images/products/laptop.jpeg", CategoryId = electronicsId },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000102"), Name = "Mouse", Description = "Magic mouse", Price = 80, StockQuantity = 50, ImageUrl = "/images/products/mouse.jpeg", CategoryId = electronicsId },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000103"), Name = "Samsung 4K Smart TV", Description = "55 inch UHD TV", Price = 900, StockQuantity = 25, ImageUrl = "/images/products/monitor.jpeg", CategoryId = electronicsId },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000104"), Name = "Sony Headphones", Description = "Noise cancelling headphones", Price = 250, StockQuantity = 60, ImageUrl = "/images/products/airpods.jpeg", CategoryId = electronicsId },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000105"), Name = "Mechanical Keyboard", Description = "RGB gaming keyboard", Price = 150, StockQuantity = 70, ImageUrl = "/images/products/keyboard.jpeg", CategoryId = electronicsId },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000035"), Name = "Men Casual Shirt", Description = "Comfortable cotton casual shirt", Price = 40, StockQuantity = 120, ImageUrl = "/images/products/menCasualShirt.jpeg", CategoryId = clothesId },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000036"), Name = "Women Handbag", Description = "Stylish leather handbag", Price = 85, StockQuantity = 70, ImageUrl = "/images/products/leatherHandbag.jpeg", CategoryId = clothesId },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000037"), Name = "Men Leather Belt", Description = "Classic brown leather belt", Price = 30, StockQuantity = 150, ImageUrl = "/images/products/brownLeatherBelt.jpeg", CategoryId = clothesId },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000038"), Name = "Men Winter Jacket", Description = "Warm winter jacket for cold weather", Price = 180, StockQuantity = 45, ImageUrl = "/images/products/winterJacket.jpeg", CategoryId = clothesId },
                new Product { Id = Guid.Parse("10000000-0000-0000-0000-000000000039"), Name = "Women Cardigan", Description = "Soft knitted cardigan", Price = 65, StockQuantity = 90, ImageUrl = "/images/products/knittedCardigan.jpeg", CategoryId = clothesId },
                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000045"),
                    Name = "Clean Architecture",
                    Description = "Guide to building maintainable software architecture by Robert C. Martin",
                    Price = 55,
                    StockQuantity = 70,
                    ImageUrl = "/images/products/CleanArchitecture.jpeg",
                    CategoryId = booksId
                },
                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000046"),
                    Name = "The Pragmatic Programmer",
                    Description = "Best practices and tips for professional programmers",
                    Price = 60,
                    StockQuantity = 50,
                    ImageUrl = "/images/products/PragmaticProgrammer.jpeg",
                    CategoryId = booksId
                },

                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000047"),
                    Name = "Introduction to Algorithms",
                    Description = "Comprehensive guide to algorithms and data structures",
                    Price = 90,
                    StockQuantity = 40,
                    ImageUrl = "/images/products/algorithms-book.jpeg",
                    CategoryId = booksId
                },

                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000048"),
                    Name = "Designing Data-Intensive Applications",
                    Description = "Modern systems design and distributed data systems",
                    Price = 75,
                    StockQuantity = 35,
                    ImageUrl = "/images/products/data-intensive.png",
                    CategoryId = booksId
                },

                new Product
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000049"),
                    Name = "Head First Design Patterns",
                    Description = "Beginner friendly explanation of software design patterns",
                    Price = 65,
                    StockQuantity = 60,
                    ImageUrl = "/images/products/head-first-design-patterns.jpeg",
                    CategoryId = booksId
                }
            );
        }
    }
}
