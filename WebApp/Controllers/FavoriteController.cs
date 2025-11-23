using Microsoft.AspNetCore.Mvc;
using Core.Contracts;
using global::Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApp.Controllers
{


    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int carId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool liked = await _favoriteService.ToggleFavoriteAsync(carId, userId);

            return Json(new { liked });
        }
    }

}
