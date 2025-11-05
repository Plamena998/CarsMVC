using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCars.Services
{
    public class CarService

    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "K5N1+QDXMcOow3vqjNd9GA==rxRpXoxqU2IwfgOQ";

        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", ApiKey);
        }

        public async Task<string> GetCarsAsync(string make = "Toyota")
        {
            var url = $"https://api.api-ninjas.com/v1/cars?make={Uri.EscapeDataString(make)}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
