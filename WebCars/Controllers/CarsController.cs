using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCars.Services;

namespace WebCars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsApiController : ControllerBase
    {
        private readonly CarService _service;

        public CarsApiController(CarService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var data = await _service.GetCarsAsync();
            return Content(data, "application/json");
        }
    }
}
