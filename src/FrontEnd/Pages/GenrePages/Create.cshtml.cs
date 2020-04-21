using FilmReference.DataAccess;
using FilmReference.FrontEnd.Managers;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.GenrePages
{
    public class CreateModel : PageModel
    {
        private readonly IGenrePagesManager _genrePagesManager;
        public CreateModel(IGenrePagesManager genrePagesManager) =>
            _genrePagesManager = genrePagesManager;
        
        public Genre Genre { get; set; }

        public IActionResult OnGet() => 
            Page();
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var newGenre = new Genre();

            var updated = await TryUpdateModelAsync(
                newGenre,
                nameof(Genre),
                g => g.GenreId, g => g.Name, g => g.Description);
            
            if (!updated)
                return Page();
            
            if (await _genrePagesManager.SaveGenre(newGenre))
                return RedirectToPage(PageValues.FilmIndexPage);

            ModelState.AddModelError(PageValues.GenreName, PageValues.DuplicateGenre);
           
            return Page();
        }
    }
}