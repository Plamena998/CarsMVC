using Core.Contracts;
using Microsoft.AspNetCore.Mvc;


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
            var topCars = await _carService.GetTop10CarsByYear(1996);

            return View(topCars);
        }
    }
}
