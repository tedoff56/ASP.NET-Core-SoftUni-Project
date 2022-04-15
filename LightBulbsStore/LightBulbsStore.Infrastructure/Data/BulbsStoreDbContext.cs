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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Cart>()
            .Navigation(c => c.CartProducts)
            .AutoInclude();

        builder.Entity<CartProduct>()
            .Navigation(cp => cp.Product)
            .AutoInclude();

        builder.Entity<Product>()
            .Navigation(p => p.Category)
            .AutoInclude();

        builder.Entity<Customer>()
            .Navigation(c => c.User)
            .AutoInclude();

        builder.Entity<Order>()
            .Navigation(o => o.Products)
            .AutoInclude();

        builder.Entity<Order>()
            .Navigation(o => o.Customer)
            .AutoInclude();

        builder
            .Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}