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

        public async Task<List<CarViewModel>> GetCarsAsync(int year = 1996)
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


                try
                {
                    var response = await _httpClient.GetAsync(url);

                    Console.WriteLine($"🔍 Requesting {make} ({year}) → {response.StatusCode}");

                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"📦 JSON for {make}: {json}\n");

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"⚠️ API Error for {make}: {response.StatusCode}");
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(json))
                    {
                        Console.WriteLine($"⚠️ Empty JSON for {make}");
                        continue;
                    }

                    List<CarViewModel>? cars = null;

                    try
                    {
                        cars = JsonSerializer.Deserialize<List<CarViewModel>>(json, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            AllowTrailingCommas = true
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ JSON deserialization error for {make}: {ex.Message}");
                    }

                    if (cars != null && cars.Count > 0)
                    {
                        allCars.AddRange(cars);
                        Console.WriteLine($"✅ Added car: {make} ({cars[0].Model})");
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ No cars parsed for {make}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Exception for {make}: {ex.Message}");
                }

                if (allCars.Count >= 10)
                    break;
            }

            // Ако API-то върне по-малко от 6 коли → добавяме примерни
            if (allCars.Count < 10)
            {
                int missing = 10 - allCars.Count;
                for (int i = 0; i < missing; i++)
                {
                    allCars.Add(new CarViewModel
                    {
                        Make = "Sample",
                        Model = $"Demo Car {i + 1}",
                        Year = year,
                        Class = "Example",
                        Fuel_Type = "Gas",
                        Transmission = "A",
                        Drive = "FWD"
                    });
                }

                Console.WriteLine($"ℹ️ Added {missing} sample cars to reach 6 total.\n");
            }

            Console.WriteLine($"✅ Returning {allCars.Count} total cars.\n");
            return allCars.Take(10).ToList();
        }
    }
}
