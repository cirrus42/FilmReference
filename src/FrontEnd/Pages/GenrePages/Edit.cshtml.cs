using FilmReference.DataAccess;
using FilmReference.FrontEnd.Managers;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.GenrePages
{
    public class EditModel : PageModel
    {
        private readonly IGenrePagesManager _genrePagesManager;
        public Genre Genre { get; set; }
        public EditModel(IGenrePagesManager genrePagesManager) => 
            _genrePagesManager = genrePagesManager;
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var results  = await _genrePagesManager.GetGenreById(id.Value);

            if (results.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            Genre = results.Entity;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            if (!ModelState.IsValid)
                return Page();

            var results = await _genrePagesManager.GetGenreById(id.Value);

            if (results.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            Genre = results.Entity;

            var updated = await TryUpdateModelAsync(
                Genre,
                nameof(Genre),
                g => g.GenreId, g => g.Name, g => g.Description);

            if (!updated)
                return Page();

            if (await _genrePagesManager.UpdateGenre(Genre))
                return RedirectToPage(PageValues.GenreIndexPage);

            ModelState.AddModelError(PageValues.GenreName, PageValues.DuplicateGenre);
            return Page();
        }
    }
}