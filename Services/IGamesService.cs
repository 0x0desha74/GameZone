namespace GameZone.Services
{
    public interface IGamesService
    {
        Task<IEnumerable<Game>> GetAll();
        Task<Game?> GetById(int Id);
        Task Create(CreateGameFormViewModel model);
        Task<Game?> Update(EditGameFormViewModel model);
        bool Delete(int Id);
    }
}
