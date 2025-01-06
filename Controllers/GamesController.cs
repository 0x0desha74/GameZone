
namespace GameZone.Controllers
{

    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesService _devicesService;
        private readonly IGamesService _gamesService;

        public GamesController(ApplicationDbContext context, ICategoriesService categoriesService, IDevicesService devicesService, IGamesService gamesService)
        {
            _context = context;
            _categoriesService = categoriesService;
            _devicesService = devicesService;
            _gamesService = gamesService;
        }
        public async Task<IActionResult> Index()
        {
            var games = await _gamesService.GetAll();

            return View(games);
        }


        public async Task<IActionResult> Details(int Id)
        {
            var game = await _gamesService.GetById(Id);
            if (game is null)
                return NotFound();
            return View(game);
        }

        public IActionResult Create()
        {

            CreateGameFormViewModel ViewModel = new()
            {
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList(),
            };
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            model.Categories = _categoriesService.GetSelectList();
            model.Devices = _devicesService.GetSelectList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _gamesService.Create(model);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _gamesService.GetById(id);
            if (game is null)
                return NotFound();
            EditGameFormViewModel viewModel = new()
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList(),
                SelectedDevices = game.Devices.Select(d =>d.DeviceId).ToList(),
                CurrentCover = game.Cover,
            };

            return View(viewModel);

        }
        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            model.Categories = _categoriesService.GetSelectList();
            model.Devices = _devicesService.GetSelectList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

          var game = await _gamesService.Update(model);

            if (game is null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }
        //[HttpDelete]
        public IActionResult Delete(int Id)
        {

            var isDeleted = _gamesService.Delete(Id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}