using LightBulbsStore.Infrastructure.Data;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        if (dbContext.Categories.Any())
        {
            return;
        }

        var categories = new List<Category>()
        {
            new (){Name = "LED КРУШКИ"},
            new (){Name = "LED ПУРИ"},
            new (){Name = "LED ПАНА"},
            new (){Name = "LED ПАНЕЛИ"},
            new (){Name = "LED ПЛАФОНИ"},
            new (){Name = "LED ПРОЖЕКТОРИ"},
            new (){Name = "LED НЕОНИ"},
            new (){Name = "LED ЛУНИЧКИ"},
            new (){Name = "LED ЛЕНТИ"},
            new (){Name = "LED УЛИЧНО ОСВЕТЛЕНИЕ"},
            new (){Name = "ЗАХРАНВАНИЯ"},
            new (){Name = "КОНТРОЛЕРИ"}
        };

        dbContext.AddRange(categories);
        dbContext.SaveChanges();
    }
}