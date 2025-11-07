using Microsoft.AspNetCore.Mvc;
using WebApp.Contracts;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarsService _carService;

        public HomeController(ICarsService carService)
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
