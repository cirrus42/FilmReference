using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class IndexModel : FilmReferencePageModel
    {
        public IndexModel(FilmReferenceContext context)
            : base (context)
        {
        }

        public IList<Studio> Studio { get;set; }

        public async Task OnGetAsync()
            => Studio = await _context.Studio
                .OrderBy(s => s.Name)
                .ToListAsync();
    }
}
