
using ECommerce.Domain.Entities;
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
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Admin",
                    Email = "admin@admin.com",
                    PasswordHash = "admin123",
                    Role = "Admin"
                },
                new User
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Buyer",
                    Email = "buyer@buyer.com",
                    PasswordHash = "buyer123",
                    Role = "Buyer"
                }
            );
        }
    }
}
