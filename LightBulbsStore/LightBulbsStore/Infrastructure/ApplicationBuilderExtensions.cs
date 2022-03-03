using LightBulbsStore.Data;
using LightBulbsStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LightBulbsStore.Infrastructure;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder PrepareDatabase(
        this IApplicationBuilder applicationBuilder)
    {
        using var scopedServices = applicationBuilder.ApplicationServices.CreateScope();

        var data = scopedServices.ServiceProvider.GetService<BulbsStoreDbContext>();
        
        data.Database.Migrate();

        SeedCategories(data);
        
        return applicationBuilder;
    }

    private static void SeedCategories(BulbsStoreDbContext dbContext)
    {

        if(!dbContext.Categories.Any())
        {
            return;
        }

        dbContext.Categories.AddRange(new[]
        {
            new Category(){Name = "LED КРУШКИ"},
            new Category(){Name = "LED ПУРИ"},
            new Category(){Name = "LED ПАНА"},
            new Category(){Name = "LED ПАНЕЛИ"},
            new Category(){Name = "LED ПЛАФОНИ"},
            new Category(){Name = "LED ПРОЖЕКТОРИ"},
            new Category(){Name = "LED НЕОНИ"},
            new Category(){Name = "LED ЛУНИЧКИ"},
            new Category(){Name = "LED ЛЕНТИ"},
            new Category(){Name = "LED УЛИЧНО ОСВЕТЛЕНИЕ"},
            new Category(){Name = "ЗАХРАНВАНИЯ"},
            new Category(){Name = "КОНТРОЛЕРИ"}
        });
    }
}