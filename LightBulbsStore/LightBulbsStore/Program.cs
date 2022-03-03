using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LightBulbsStore.Data;
using LightBulbsStore.Infrastructure;
using LightBulbsStore.Services;
using LightBulbsStore.Services.Contracts;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration
        .GetConnectionString("DefaultConnection");
    
    builder.Services
        .AddDbContext<BulbsStoreDbContext>(options => options.UseSqlServer(connectionString));
    
    builder.Services
        .AddDatabaseDeveloperPageExceptionFilter();

    builder.Services
        .AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        })
        .AddEntityFrameworkStores<BulbsStoreDbContext>();

    builder.Services
            .AddControllersWithViews();

    builder.Services
        .AddTransient<IProductService, ProductService>();
    builder.Services.AddSingleton<ITextShortenerService, TextShortenerService>();

    var app = builder.Build();

    app.PrepareDatabase();
// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app
        .UseHttpsRedirection()
        .UseStaticFiles()
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseEndpoints(ep =>
        {
            ep.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            ep.MapRazorPages();
        });
    
    app.Run();