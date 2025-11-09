using Microsoft.Extensions.Options;
using System.Text.Json;
using Core.Contracts;
using Core.Models;

namespace Services
{
    public class CarService : ICarsService
    {
        private readonly HttpClient _httpClient;
        private readonly AppOptions _options;

        public CarService(HttpClient httpClient, IOptions<AppOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }
        public async Task<List<CarViewModel>> GetCarsAsync(int year)
        {
            // Добавя ключа само веднъж
            if (!_httpClient.DefaultRequestHeaders.Contains("X-Api-Key"))
                _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _options.ApiKey);

            var makes = new[]
            {
            "toyota", "honda", "ford", "bmw", "audi",
            "nissan", "mazda", "chevrolet", "mercedes", "kia",
            "hyundai", "volkswagen", "subaru", "volvo", "lexus",
            "jaguar", "porsche", "fiat", "peugeot", "renault",
            "mitsubishi", "landrover", "tesla", "infiniti", "acura",
            "mini", "seat", "skoda", "citroen", "alfa romeo"
            };

            // Създава паралелни задачи за всички марки
            var tasks = makes.Select(async make =>
            {
                string url = $"{_options.Url}?make={make}&year={year}";
                try
                {
                    var response = await _httpClient.GetAsync(url);
                    if (!response.IsSuccessStatusCode) return new List<CarViewModel>();

                    var json = await response.Content.ReadAsStringAsync();
                    var cars = JsonSerializer.Deserialize<List<CarViewModel>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        AllowTrailingCommas = true
                    }) ?? new List<CarViewModel>();
                    return cars;
                }
                catch
                {
                    return new List<CarViewModel>();
                }
            });

            // Изпълняваме всички заявки едновременно
            var results = await Task.WhenAll(tasks);
            return results.SelectMany(x => x).ToList();
        }

        // Връща топ 10 коли по конкретна година (само за визуализация)
        public async Task<List<CarViewModel>> GetTop10CarsByYear(int year)
        {
            var allCars = await GetCarsAsync(year);
            return allCars.Take(10).ToList();
        }
    }
}
