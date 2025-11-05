using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarService _carService;

        public HomeController(CarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetCarsAsync(1996);
            return View(cars);

        }
    }
}
