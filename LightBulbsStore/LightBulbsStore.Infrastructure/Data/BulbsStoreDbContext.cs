using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
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

        builder.Entity<CartProduct>()
            .HasKey(cp => new
            {
                cp.CartId,
                cp.ProductId
            });

        builder.Entity<OrderProduct>()
            .HasKey(op => new
            {
                op.OrderId,
                op.ProductId
            });

        builder.Entity<User>()
            .Navigation(u => u.Customer)
            .AutoInclude();

        builder.Entity<Customer>()
            .Navigation(c => c.Cart)
            .AutoInclude();

        builder.Entity<Cart>()
            .Navigation(c => c.Products)
            .AutoInclude();

        builder.Entity<CartProduct>()
            .Navigation(cp => cp.Product)
            .AutoInclude();

        builder.Entity<Product>()
            .Navigation(p => p.Category)
            .AutoInclude();

        builder.Entity<Order>()
            .Navigation(o => o.Products)
            .AutoInclude();

        builder.Entity<Order>()
            .Navigation(o => o.Customer)
            .AutoInclude();

        builder.Entity<Customer>()
            .Navigation(c => c.Orders)
            .AutoInclude();

        builder
            .Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        SeedRoles(builder);

    }

    private void SeedRoles(ModelBuilder builder)
    {
        builder
            .Entity<IdentityRole>()
            .HasData(
            new IdentityRole()
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityRole()
            {
                Name = "Customer",
                NormalizedName = "CUSTOMER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
    }
}