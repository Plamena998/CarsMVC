using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IFavoriteService
    {
        public Task<bool> ToggleFavoriteAsync(int carId, string userId);
        public Task<bool> IsFavoriteAsync(int carId, string userId);
        public Task<List<int>> GetFavoriteCarIdsAsync(string userId);
    }
}
