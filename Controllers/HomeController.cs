using GameZone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGamesService _gamesService;

        public HomeController(ILogger<HomeController> logger, IGamesService gamesService)
        {
            _logger = logger;
            _gamesService = gamesService;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _gamesService.GetAll();
          
            return View(games);
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
