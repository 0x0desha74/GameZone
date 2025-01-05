
namespace GameZone.Models
{
    public class Device : BaseEntity
    {
        
        [MaxLength(100)]
        public string Icon { get; set; } = default!;
        public ICollection<Game> Games { get; set; } = new List<Game>();
        ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();

    }
}
