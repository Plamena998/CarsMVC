using Core;
using Core.Contracts;
using Core.Models;
using Services.Repositories;

namespace Services
{
    public class CarService : ICarsService
    {
        private readonly IRepository _carRepository;

        public CarService(IRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // Мапване от Car към CarViewModel
        private CarViewModel MapToViewModel(Car c)
        {
            return new CarViewModel
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year,
                Class = c.Class,
                Drive = c.Drive,
                Transmission = c.Transmission,
                Fuel_Type = c.Fuel_Type,
                City_Mpg = c.City_Mpg,
                Combination_Mpg = c.Combination_Mpg,
                Highway_Mpg = c.Highway_Mpg,
                Cylinders = c.Cylinders,
                Displacement = c.Displacement
            };
        }

        // Връща топ 10 коли от дадена година
        public async Task<List<CarViewModel>> GetTop10CarsByYear(int year)
        {
            var cars = await _carRepository.GetTop10CarsByYear(year);
            return cars.Select(MapToViewModel).ToList();
        }

        // Връща кола по ID за модалния прозорец
        public async Task<CarViewModel?> GetCarByIdAsync(int id)
        {
            var car = await _carRepository.GetCarByIdAsync(id);

            if (car == null)
                return null;

            return MapToViewModel(car);
        }

    }
}
