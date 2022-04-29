using LightBulbsStore.Infrastructure.Data;
using LightBulbsStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using LightBulbsStore.Infrastructure.Data.Enumerations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LightBulbsStore.Extensions;

public static class ApplicationBuilderExtensions
{

    public static IApplicationBuilder PrepareDatabase(
        this IApplicationBuilder applicationBuilder)
    {
        using var scopedServices = applicationBuilder.ApplicationServices.CreateScope();

        var data = scopedServices.ServiceProvider.GetService<BulbsStoreDbContext>();

        data.Database.Migrate();

        // Configuration.cs, Seed method.

        SeedCategories(data);
        SeedProducts(data);

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
            new (){Name = "LED Крушки"},
            new (){Name = "LED Ленти"},
            new (){Name = "LED Лунички"},
            new (){Name = "LED Плафони"},
            new (){Name = "LED Прожектори"},
            new (){Name = "LED Панели"},
            new (){Name = "LED Пури"},
        };

        dbContext.AddRange(categories);
        dbContext.SaveChanges();
    }


    private static void SeedProducts(BulbsStoreDbContext dbContext)
    {
        if (dbContext.Products.Any())
        {
            return;
        }

        var products = new List<Product>()
        {
            new (){
                Name = "60WЕ27 МАТ А60",
                Price = 6.49M,
                Description = @"9 W, 220 V, E27, LED, 806 lm, 2700 К, топла светлина, 15 000 h живот,
                                енергиен клас А+, без димиране, Philips, Китай, 2 години гаранция",
                ImageUrl = "https://mr-bricolage.bg/medias/sys_master/products/h92/h41/8842236985374.jpg",
                Category = dbContext.Categories.First(c => c.Name == "LED Крушки"),
            },
            new()
            {
                Name = "40W E14B35 2700K",
                Price = 5.99M,
                Description = @"40W, E14, светлинен поток 470 lm, топла светлина, продължителност на живот 15 000ч",
                ImageUrl = "https://mr-bricolage.bg/medias/sys_master/products/ha5/h98/8844016844830.jpg",
                Category = dbContext.Categories.First(c => c.Name == "LED Крушки")
            },
            new()
            {
                Name = "14.4W/m 12VDC 300SMD 5050 IP20 50m IP20 LED LABS 16-1119-01",
                Price = 7.99M,
                Description = @"RGB LED лента 14.4W LED LABS 16-1119-01 - висок клас лед лента излъчваща RGB светлина в ролка от 50 метра. Интегрирани диоди SMD5050 с 
                                възможност за рязане през 3 броя. Подходяща за подсветка в окачени тавани и ниши, както и за индиректно осветление в окачени тавани. 
                                Широчината на LED лентата позволява инсталиране в лед профил с широчина над 10 милиметра. Лентата се доставя на ролка от 50 метра, като се продава се на линеен метър. 
                                Минималната дължина при поръчка е 1 линеен метър!",
                ImageUrl = "https://www.led-zona.bg/uploads/com_article/gallery/thumbs/500x500_720c5e59b64ae4c456fd79d15e9ca148f65d9891.jpg",
                Category = dbContext.Categories.First(c => c.Name == "LED Ленти")
            },
            new()
            {
                Name = "6W/m, 12VDC, IP20, 90SMD/m, 360lm",
                Price = 80.38M,
                Description = @"Комплект Smart WIFI LED лента RGB+CCT LED лента с дистанционно.
                                Включва: LED лента, Wifi контролер и 12V захранване.
                                Този комплект Wifi LED лента се предлага с всичко необходимо, за да свържете и настроите бързо вашите LED ленти. Контролерът използва Wifi технология, която ви позволява да се свържете с вашия телефон или смарт устройство чрез приложение. Свържете комплекта си за Wifi LED лента към Alexa или Google Home, без да е необходим хъб, тъй като можете също да се свържете с мобилното си устройство чрез Alexa или Google Assistant. Налични милиони цветови комбинации за създаване на перфектната атмосфера във вашия дом или офис.",
                ImageUrl = "https://www.led-zona.bg/uploads/com_article/gallery/thumbs/500x500_ba293d35532fafeef9067a865192fdcb970c317d.jpg",
                Category = dbContext.Categories.First(c => c.Name == "LED Ленти")
            },
            new()
            {
                Name = "Ultralux LZ10627, 6W, 440lm, GU10, 2700K, 220V-240V AC, 15°",
                Price = 7.86M,
                Description = @"Спот луничка за насочено осветление Ultralux LZ10627 - лед луничка за акцентно осветяване излъчваща топла 2700К светлина. Специално изработения корпус от алуминий допълнен с термопроводима пластмаса гарантират много добро охлаждане. Подходяща за директна подмяна на халогенни лампи с форма PAR16 и цокъл GU10.",
                ImageUrl = "https://www.led-zona.bg/uploads/com_article/gallery/thumbs/500x500_3caa122ccec64db0da784b83f23e809a53a98480.jpg",
                Category = dbContext.Categories.First(c => c.Name == "LED Лунички")
            },
            new()
            {
                Name = "60W 230V МУЛТИФУНКЦИОНАЛЕН",
                Price = 87.99M,
                Description = @"Функция димиране и смяна на цветната температура, дистанционно
управление, 60W, Ø463, 230V, IP20, 120 ͦ",
                ImageUrl = "https://mr-bricolage.bg/medias/sys_master/products/hd5/h48/h00/8882814582814.jpg",
                Category = dbContext.Categories.First(c => c.Name == "LED плафони")
            },
            new()
            {
                Name = "100W = 600W V-tac 8500 лумена",
                Price = 33.99M,
                Description = @"Тип Светлина –  3000k(топла) или 4000к ( Неутрална) или 6400к ( Студено бяла)",
                ImageUrl = "https://kabelelektrik.com/wp-content/uploads/2019/07/v-tac-100W-ledprojektor-600x600.jpeg",
                Category = dbContext.Categories.First(c => c.Name == "LED прожектори")
            },
            new()
            {
                Name = "LED Прожектор 10W V-tac",
                Price = 5.5M,
                Description = @"Тип Светлина – 6400к ( Студено бяла) 4000к ( Неутрална) или 3000к(топла)",
                ImageUrl = "https://kabelelektrik.com/wp-content/uploads/2020/09/LED-Projektor-10W-Evtin-600x600.jpeg",
                Category = dbContext.Categories.First(c => c.Name == "LED прожектори")
            },
            new()
            {
                Name = "LED Панел V-tac 45W 60*60см",
                Price = 42.0M,
                Description = @"LED Пано V-tac 45W 60*60см за скрит монтаж, заменящ осветителите за растерен таван",
                ImageUrl = "https://kabelelektrik.com/wp-content/uploads/2019/03/LED-Panel-600x600-36W-Hi-Lumen-1-600x600.jpg",
                Category = dbContext.Categories.First(c => c.Name == "LED Панели")
            },
            new()
            {
                Name = "LED пура T8 18W4000K",
                Price = 7.0M,
                Description = @"Moщност (W): 80
Сила на светене lm: 638
Цветна температура: топла
Дължина мм: 600",
                ImageUrl = "https://www.home-max.bg/static/media/ups/cached/f73efacb6c98dc98eafbb9fd43234c428d33cda0.jpg",
                Category = dbContext.Categories.First(c => c.Name == "LED Пури")
            }

        };

        dbContext.AddRange(products);
        dbContext.SaveChanges();
    }
}