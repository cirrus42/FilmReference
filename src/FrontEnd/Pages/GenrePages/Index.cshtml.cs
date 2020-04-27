using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.DataAccess.DbClasses;

namespace FilmReference.FrontEnd.Pages.GenrePages
{
    public class IndexModel : PageModel
    {
        private readonly IGenreHandler _genreHandler;
        public IList<GenreEntity> Genre { get; set; }

        public IndexModel(IGenreHandler genreHandler) =>
            _genreHandler = genreHandler;

        public async Task OnGetAsync() =>
            Genre = (await _genreHandler.GetGenres()).ToList();
    }
}