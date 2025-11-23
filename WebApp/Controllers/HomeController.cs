using Core;
using Core.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;


namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarsService _carService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFavoriteService _favoritesService;

        public HomeController(ICarsService carService, UserManager<ApplicationUser> userManager, IFavoriteService favoriteService)
        {
            _carService = carService;
            _userManager = userManager;
            _favoritesService = favoriteService;
        }

        public async Task<IActionResult> Index()
        {
            var topCars = await _carService.GetTop10CarsByYear(1996);

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var favoriteCarIds = await _favoritesService.GetFavoriteCarIdsAsync(user.Id);

                foreach (var car in topCars)
                {
                    car.IsLiked = favoriteCarIds.Contains(car.Id);
                }
            }

            return View(topCars);
        }
    }
}
