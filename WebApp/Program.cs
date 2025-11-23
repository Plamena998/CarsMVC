using Core;
using Core.Contracts;
using Core.Models;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Repositories;
using WebApp.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация на services
builder.Services.AddControllersWithViews();

// Зареждаме настройките от appsettings.json → AppOptions
builder.Services.Configure<AppOptions>(builder.Configuration.GetSection(AppOptions.ApiParameters));

// Регистрираме DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();

// Регистрираме services
builder.Services.AddScoped<IAppEnvironment, AppEnvironment>();
builder.Services.AddScoped<FileManager>();
builder.Services.AddScoped<IRepository, CarRepository>();
builder.Services.AddScoped<ICarsService, CarService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();

// HttpClient не е нужен, ако вече не използваме API
// builder.Services.AddHttpClient<CarService>();

var app = builder.Build();

// Seed-ване на базата при стартиране (локални фиктивни данни)
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//    if (!db.cars.Any())
//    {
//        db.cars.AddRange(new List<Car>
//        {
//            new Car { Make = "Toyota", Model = "Corolla", Year = 2020, Class = "Sedan", Drive = "FWD", Transmission = "Automatic", Fuel_Type = "Petrol", City_Mpg = 30, Highway_Mpg = 38, Combination_Mpg = 33, Cylinders = 4, Displacement = 1.8 },
//            new Car { Make = "Honda", Model = "Civic", Year = 2021, Class = "Sedan", Drive = "FWD", Transmission = "Automatic", Fuel_Type = "Petrol", City_Mpg = 32, Highway_Mpg = 42, Combination_Mpg = 36, Cylinders = 4, Displacement = 2.0 },
//            new Car { Make = "Ford", Model = "Mustang", Year = 2022, Class = "Coupe", Drive = "RWD", Transmission = "Manual", Fuel_Type = "Petrol", City_Mpg = 18, Highway_Mpg = 27, Combination_Mpg = 21, Cylinders = 8, Displacement = 5.0 },
//            new Car { Make = "Tesla", Model = "Model 3", Year = 2023, Class = "Sedan", Drive = "RWD", Transmission = "Automatic", Fuel_Type = "Electric", City_Mpg = 130, Highway_Mpg = 120, Combination_Mpg = 125, Cylinders = 0, Displacement = 0 }
//            // можеш да добавиш още фиктивни коли тук
//        });

//        await db.SaveChangesAsync();
//    }
//}

// Middleware конфигурация
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

await app.RunAsync();
