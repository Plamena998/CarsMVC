using Core;
using Core.Contracts;
using DataContext;
using Microsoft.EntityFrameworkCore;


namespace Services
{
    public class DataSeed(AppDbContext db, ICarsService carService)
    {
        private readonly AppDbContext _db = db;
        private readonly ICarsService _carService = carService;

        public async Task SeedAllCarsAsync()
        {
            await SeedCarsFromApiAsync(); // всички коли от API
            //await SeedCarsFromLocalImagesAsync();
        }
        private async Task SeedCarsFromApiAsync()
        {
            var carsFromApi = await _carService.GetCarsAsync(1996); // ГОДИНА 2

            foreach (var car in carsFromApi)
            {
                bool exists = await _db.cars.AnyAsync(c =>
                    c.Make == car.Make &&
                    c.Model == car.Model &&
                    c.Year == car.Year);

                if (exists) continue;

                _db.cars.Add(new Car
                {
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    Class = car.Class,
                    Transmission = car.Transmission,
                    Drive = car.Drive,
                    Fuel_Type = car.Fuel_Type,
                    ImageUrl = car.ImageUrl ?? "/images/no-image.jpg",
                    ImageGuid = Guid.NewGuid().ToString("N")
                });
            }

            await _db.SaveChangesAsync();
            Console.WriteLine($"Cars fetched from API: {carsFromApi.Count}");

        }

        /*private async Task SeedCarsFromLocalImagesAsync()
        {
            var imagesPath = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(imagesPath)) return;

            var files = Directory.GetFiles(imagesPath)
                                 .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png"))
                                 .ToList();

            foreach (var file in files)
            {
                string relativePath = "/images/" + Path.GetFileName(file);

                bool exists = await _db.cars.AnyAsync(c => c.ImageUrl == relativePath);
                if (exists) continue;

                _db.cars.Add(new Car
                {
                    Make = "Unknown",
                    Model = Path.GetFileNameWithoutExtension(file),
                    Year = DateTime.Now.Year,
                    Class = "Imported",
                    Transmission = "N/A",
                    Drive = "N/A",
                    Fuel_Type = "N/A",
                    ImageUrl = relativePath,
                    ImageGuid = Guid.NewGuid().ToString("N")
                });
            }

            await _db.SaveChangesAsync();
        }*/
    }
}
