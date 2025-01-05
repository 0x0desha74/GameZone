
namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;


        public GamesService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _context.Games
                 .AsNoTracking()
                 .Include(G => G.Category)
                 .Include(G => G.Devices)
                 .ThenInclude(D => D.Device)
                 .ToListAsync();

        }
        public async Task<Game?> GetById(int Id)
        {
            return await _context.Games
                .AsNoTracking()
                .Include(G => G.Category)
                .Include(G => G.Devices)
                .ThenInclude(D => D.Device)
                .SingleOrDefaultAsync(G => G.Id == Id);
        }
        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };
            await _context.AddAsync(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = _context.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == model.Id);
            if (game is null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();
            if (hasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }
            var affectedRows = _context.SaveChanges();
            if (affectedRows > 0)
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
                return null;
            }
        }


        public bool Delete(int Id)
        {
            var isDeleted = false;
            var game = _context.Games.Find(Id);
            var coverName = game.Cover;

            if (game is null)
                return isDeleted;

            _context.Remove(game);
            var affectedRows = _context.SaveChanges();
            if (affectedRows > 0)
            {
                var cover = Path.Combine(_imagesPath, coverName);
                File.Delete(cover);
                isDeleted = true;
            }
            return isDeleted;

        }


        private async Task<string> SaveCover(IFormFile file)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var path = Path.Combine(_imagesPath, coverName);

            using var stream = File.Create(path);
            await file.CopyToAsync(stream);
            return coverName;
        }


    }
}
