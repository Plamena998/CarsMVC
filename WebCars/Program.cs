using WebCars.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<CarService>();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();
