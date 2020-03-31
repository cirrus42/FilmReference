using FilmReference.DataAccess;
using FilmReference.FrontEnd.Classes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Pages.GenrePages
{
    public class IndexModel : FilmReferencePageModel
    {
        public IndexModel(FilmReferenceContext context)
            : base (context)
        {
        }

        public IList<Genre> Genre { get;set; }

        public async Task OnGetAsync()
            => Genre = await _context.Genre
                .OrderBy(g => g.Name)
                .ToListAsync();
    }
}
