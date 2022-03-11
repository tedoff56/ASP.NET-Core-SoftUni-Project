using LightBulbsStore.Data.Models;
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
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder
            .Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();
        
        builder
            .Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}