using Core;
using Core.Contracts;
using Core.Models;
using DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public class CarRepository : IRepository
    {
        private readonly AppDbContext _db;
        public CarRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Car>> GetAllCars()
        {
            return await _db.cars.ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await _db.cars.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Car>> GetTop10CarsByYear(int year)
        {
            return await _db.cars
                            .Where(c => c.Year == year)
                            .OrderByDescending(c => c.Id)
                            .Take(10)
                            .ToListAsync();
        }
    }
}
