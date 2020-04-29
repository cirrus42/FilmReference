using FilmReference.DataAccess.DbClasses;
using FilmReference.FrontEnd.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Shared.Models;

namespace FilmReference.FrontEnd.Pages.GenrePages
{
    public class CreateModel : PageModel
    {
        private readonly IGenrePagesManager _genrePagesManager;
        public CreateModel(IGenrePagesManager genrePagesManager) =>
            _genrePagesManager = genrePagesManager;
        
        public GenreEntity Genre { get; set; }

        public IActionResult OnGet() => 
            Page();
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var newGenre = new GenreEntity();

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