using Core;
using DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly AppDbContext _context;

        public FavoriteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ToggleFavoriteAsync(int carId, string userId)
        {
            var fav = await _context.favoriteCars
                .FirstOrDefaultAsync(f => f.CarId == carId && f.UserId == userId);

            if (fav == null)
            {
                _context.favoriteCars.Add(new FavoriteCar
                {
                    CarId = carId,
                    UserId = userId
                });
                await _context.SaveChangesAsync();
                return true; // liked
            }
            else
            {
                _context.favoriteCars.Remove(fav);
                await _context.SaveChangesAsync();
                return false; // unliked
            }
        }

        public async Task<bool> IsFavoriteAsync(int carId, string userId)
        {
            return await _context.favoriteCars
                .AnyAsync(f => f.CarId == carId && f.UserId == userId);
        }
        public async Task<List<int>> GetFavoriteCarIdsAsync(string userId)
        {
            return await _context.favoriteCars
                .Where(f => f.UserId == userId)
                .Select(f => f.CarId)
                .ToListAsync();
        }
    }

}
