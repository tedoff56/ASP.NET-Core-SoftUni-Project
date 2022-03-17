using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LightBulbsStore.Infrastructure.Data;

public class BulbsStoreDbContext : IdentityDbContext
{
    public BulbsStoreDbContext(DbContextOptions<BulbsStoreDbContext> options)
        : base(options)
    {
    }
    

    public DbSet<Customer> Customers { get; init; }
    
    public DbSet<Product> Products { get; init; }

    public DbSet<Category> Categories { get; init; }

    public DbSet<Cart> Carts { get; init; }

    public DbSet<CartProducts> CartProducts { get; init; }

    public DbSet<Order> Orders { get; init; }

    public DbSet<OrderProduct> OrderProducts { get; init; }

    public DbSet<User> Users { get; init; }

    public DbSet<ProductStock> ProductStock { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        builder
            .Entity<OrderProduct>()
            .HasKey(op => new
            {
                op.OrderId, 
                op.ProductId
            });
        
        builder
            .Entity<CartProducts>()
            .HasKey(cp => new
            {
                cp.CartId, 
                cp.ProductId
            });
    }
}