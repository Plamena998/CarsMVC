using WebApp.Models;
using WebApp.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//  1. Зареждаме настройките от appsettings.json
builder.Services.Configure<AppOptions>(
    builder.Configuration.GetSection(AppOptions.ApiParameters));

//  2. Регистрираме AppOptions като singleton, за по-лесен достъп
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<AppOptions>>().Value);

//  3. Добавяме HttpClient за CarService
builder.Services.AddHttpClient<CarService>((serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<AppOptions>();
    if (!string.IsNullOrEmpty(options.Url))
        client.BaseAddress = new Uri(options.Url);
});

//  4. Регистрираме CarService в DI контейнера
builder.Services.AddScoped<CarService>();

//  5. Добавяме MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

//  6. Middleware пайплайн
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

//  7. Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
