using Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetCarModal(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null) return NotFound();
            return PartialView("_CarModalPartial", car);
        }
    }
}
