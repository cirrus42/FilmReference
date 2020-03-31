using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Helpers;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class IndexModel : FilmReferencePageModel
    {
        public IImageHelper ImageHelper;
        public IndexModel(FilmReferenceContext context, IImageHelper imageHelper)
            : base (context)
        {
            ImageHelper = imageHelper;
        }

        public IList<Studio> Studio { get;set; }

        public async Task OnGetAsync()
            => Studio = await _context.Studio
                .OrderBy(s => s.Name)
                .ToListAsync();
    }
}
