using GameZone.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel : GameViewModel
    {
      
        [MaxSize(FileSettings.MaxFileSizeInBytes)]
        [AllowedExtensions(FileSettings.AlllowedExtensions)]
        public IFormFile Cover { get; set; } = default!;


    }
}
