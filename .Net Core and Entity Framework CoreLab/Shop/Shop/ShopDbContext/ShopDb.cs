using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.ShopDbContext
{
    public class ShopDb : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Salesman> Salesmans { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer("Server=.;Database=ShopDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Customer>()
                .HasOne(c => c.Salesman)
                .WithMany(s => s.Customers)
                .HasForeignKey(c => c.SalesmanId);

            builder
                .Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId);

            builder
                .Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(c => c.CustomerId);

            builder
                .Entity<ItemOrder>()
                .HasKey(x => new { x.ItemId, x.OrderId });

            builder
                .Entity<Item>()
                .HasMany(i => i.Orders)
                .WithOne(io => io.Item)
                .HasForeignKey(io => io.ItemId);

            builder
                .Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(io => io.OrderId);

            builder
                .Entity<Item>()
                .HasMany(i => i.Reviews)
                .WithOne(r => r.Item)
                .HasForeignKey(r => r.ItemId);
        }
    }
}
