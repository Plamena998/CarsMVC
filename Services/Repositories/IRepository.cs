using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public interface IRepository
    {
        Task<List<Car>> GetTop10CarsByYear(int year);
        Task<Car?> GetCarByIdAsync(int id);
        Task<List<Car>> GetAllCars();
    }
}
