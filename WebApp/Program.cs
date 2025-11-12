using Core;
using Core.Contracts;
using Core.Models;
using DataContext;
using Microsoft.EntityFrameworkCore;
using Services;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация на services
builder.Services.AddControllersWithViews();

// Зареждаме настройките от appsettings.json → AppOptions
builder.Services.Configure<AppOptions>(builder.Configuration.GetSection(AppOptions.ApiParameters));



// Регистрираме DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Регистрираме
builder.Services.AddScoped<IAppEnvironment, AppEnvironment>();
builder.Services.AddScoped<FileManager>();
builder.Services.AddScoped<DataSeed>();
builder.Services.AddScoped<ICarsService, CarService>();

// HttpClient за CarService
builder.Services.AddHttpClient<CarService>();

var app = builder.Build();

// Seed-ване на базата при стартиране
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeed>();
    await seeder.SeedAllCarsAsync(); // seed-ва локални снимки + API данни
}

//  Middleware конфигурация
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//////////допълнително
var sScope = app.Services.CreateScope();
var instance = sScope.ServiceProvider.GetRequiredService<DataSeed>();
await instance.SeedAllCarsAsync();

await app.RunAsync();
