using LightBulbsStore.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LightBulbsStore.Data;

public class BulbsStoreDbContext : IdentityDbContext
{

    public DbSet<Product> Products { get; init; }
    
    public BulbsStoreDbContext(DbContextOptions<BulbsStoreDbContext> options)
        : base(options)
    {
    }
}