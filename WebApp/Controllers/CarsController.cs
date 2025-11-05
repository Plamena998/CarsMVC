using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Contracts;

namespace WebApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarsService carsService;
        public CarsController(ICarsService carsService)
        {
            this.carsService = carsService;
        }

        // Този екшън взима параметъра make от QueryString (?make=...)
        [HttpGet]
        public async Task<IActionResult> GetCars([FromQuery] string make)
        {
            var cars = await carsService.GetCarAsync(make);
            return Ok(cars);
        }
    }
}