using Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService _carService;

        public CarsController(ICarsService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarModal(string make, string model, int year)
        {
            var car = await _carService.GetCarFromLoadedDataAsync(make, model, year);

            if (car == null)
                return NotFound();

            return PartialView("_CarModalPartial", car);
        }

    }
}
