
namespace GameZone.ViewModels
{
    public class EditGameFormViewModel: GameViewModel
    {
        public int Id { get; set; }
        public string? CurrentCover { get; set; }
        [MaxSize(FileSettings.MaxFileSizeInBytes)]
        [AllowedExtensions(FileSettings.AlllowedExtensions)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
