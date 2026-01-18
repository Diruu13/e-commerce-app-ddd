using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Persistence
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().OwnsOne(c => c.Address);
            modelBuilder.Entity<Order>().OwnsOne(o => o.ShippingAddress);
            modelBuilder.Entity<OrderItem>().
                HasKey(oi => new { oi.OrderId, oi.ProductId });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.OwnsOne(o => o.ShippingAddress, sa =>
                {
                    sa.Property(p => p.Street).HasColumnName("Street");
                    sa.Property(p => p.City).HasColumnName("City");
                    sa.Property(p => p.State).HasColumnName("State");
                    sa.Property(p => p.PostalCode).HasColumnName("PostalCode");
                    sa.Property(p => p.Country).HasColumnName("Country");
                });
            });

            modelBuilder.Entity<Customer>(builder =>
            {
                builder.OwnsOne(c => c.Address, a =>
                {
                    a.Property(p => p.Street).HasColumnName("Street");
                    a.Property(p => p.City).HasColumnName("City");
                    a.Property(p => p.State).HasColumnName("State");
                    a.Property(p => p.PostalCode).HasColumnName("PostalCode");
                    a.Property(p => p.Country).HasColumnName("Country");
                });
            });

            modelBuilder.Entity<Customer>().HasData(
                new { Id = 1, FirstName = "Asep", LastName = "Sutisna", Email = "asepntis@example.com" },
                new { Id = 1, FirstName = "Tatang", LastName = "Parabola", Email = "tarbol@example.com" }
            );

            modelBuilder.Entity<Customer>().OwnsOne(c => c.Address).HasData(
                new
                {
                    CustomerId = 1,
                    Street = "Jl. Merdeka No. 1",
                    City = "Jakarta",
                    State = "DKI Jakarta",
                    PostalCode = "10110",
                    Country = "Indonesia"
                },
                new
                {
                    CustomerId = 2,
                    Street = "Jl. Sudirman No. 2",
                    City = "Bandung",
                    State = "West Java",
                    PostalCode = "40115",
                    Country = "Indonesia"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new { Id = 1, Name = "Laptop", Price = 1500.00m, StockQuantity = 10, Description = "High performance laptop" },
                new { Id = 2, Name = "Smartphone", Price = 800.00m, StockQuantity = 25, Description = "Latest model smartphone" },
                new { Id = 3, Name = "Headphones", Price = 200.00m, StockQuantity = 50, Description = "Noise-cancelling headphones" }
            );
        }
    }
}
