using FilmReference.DataAccess;
using FilmReference.FrontEnd.Classes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.DirectorPages
{
    public class IndexModel : FilmReferencePageModel
    {
        public IndexModel(FilmReferenceContext context)
            : base (context)
        {
        }

        public IList<Person> Person { get;set; }

        public async Task OnGetAsync()
        {
            Person = await _context.Person
                .Where(p => p.IsDirector)
                .OrderBy(p => p.FullName)
                .ToListAsync();
        }
    }
}
