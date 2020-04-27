using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Threading.Tasks;
using FilmReference.DataAccess.DbClasses;

namespace FilmReference.FrontEnd.Pages.GenrePages
{
    public class DetailsModel : PageModel
    {
        private readonly IGenreHandler _genreHandler;
        public GenreEntity Genre { get; set; }

        public DetailsModel(IGenreHandler genreHandler) =>
            _genreHandler = genreHandler;
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _genreHandler.GetGenreById(id.Value);

            if (result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();
           
            return Page();
        }
    }
}
