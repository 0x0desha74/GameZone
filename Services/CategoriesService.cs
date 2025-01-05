
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{

    public class CategoriesService : ICategoriesService

    {
        private readonly ApplicationDbContext _context;
        public CategoriesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
           var Categories =  _context.Categories
           .Select(C => new SelectListItem { Value = C.Id.ToString(), Text = C.Name })
           .OrderBy(C => C.Text)
           .AsNoTracking()
           .ToList();
            return Categories;
        }
    }
}
