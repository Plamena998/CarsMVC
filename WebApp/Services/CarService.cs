using System.Text.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class CarService
    {
        private readonly HttpClient _httpClient;
        private readonly AppOptions _options;

        public CarService(HttpClient httpClient, AppOptions options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<List<CarViewModel>> GetCarsAsync(int year)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _options.ApiKey);

            var makes = new[]
            {
                "toyota", "honda", "ford", "bmw", "audi",
                "nissan", "mazda", "chevrolet", "mercedes", "kia", "hyundai"
            };

            var allCars = new List<CarViewModel>();

            foreach (var make in makes)
            {
                string url = $"{_options.Url}?make={make}&year={year}";

                var response = await _httpClient.GetAsync(url);

                var json = await response.Content.ReadAsStringAsync();

                List<CarViewModel>? cars = null;

                cars = JsonSerializer.Deserialize<List<CarViewModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true
                });

                if (cars != null && cars.Count > 0)
                {
                    allCars.AddRange(cars);
                }
                if (allCars.Count >= 10) break;
            }

            //if (allCars.Count < 10)
            //{
            //    int missing = 10 - allCars.Count;
            //    for (int i = 0; i < missing; i++)
            //    {
            //        allCars.Add(new CarViewModel
            //        {
            //            Make = "Sample",
            //            Model = $"Demo Car {i + 1}",
            //            Year = year,
            //            Class = "Example",
            //            Fuel_Type = "Gas",
            //            Transmission = "A",
            //            Drive = "FWD"
            //        });
            //    }
            //}
            return allCars.Take(10).ToList();
        }
    }
}
