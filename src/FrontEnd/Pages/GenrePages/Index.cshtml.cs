using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.GenrePages
{
    public class IndexModel : PageModel
    {
        private readonly IGenrePagesManager _genrePagesManager;
        public IList<Genre> Genre { get; set; }

        public IndexModel(IGenrePagesManager genrePagesManager) =>
            _genrePagesManager = genrePagesManager;

        public async Task OnGetAsync() =>
            Genre = (await _genrePagesManager.GetGenres()).ToList();
    }
}