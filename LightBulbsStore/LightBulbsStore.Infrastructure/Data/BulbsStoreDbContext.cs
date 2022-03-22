using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LightBulbsStore.Infrastructure.Data;

public class BulbsStoreDbContext : IdentityDbContext<User>
{
    public BulbsStoreDbContext(DbContextOptions<BulbsStoreDbContext> options)
        : base(options)
    {

    }

    public DbSet<Customer> Customers { get; init; }
    
    public DbSet<Product> Products { get; init; }

    public DbSet<Category> Categories { get; init; }

    public DbSet<Cart> Carts { get; init; }

    public DbSet<CartProduct> CartProducts { get; init; }

    public DbSet<Order> Orders { get; init; }

    public DbSet<OrderProduct> OrderProducts { get; init; }

    public DbSet<Stock> ProductsStock { get; set; }
    
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
            .Entity<CartProduct>()
            .HasKey(cp => new
            {
                cp.CartId, 
                cp.ProductId
            });
    }
}