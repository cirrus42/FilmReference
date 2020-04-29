using FilmReference.FrontEnd.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using FilmReference.DataAccess.Entities;
using Shared.Models;

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
                g => g.Id, g => g.Name, g => g.Description);
            
            if (!updated)
                return Page();
            
            if (await _genrePagesManager.SaveGenre(newGenre))
                return RedirectToPage(PageValues.FilmIndexPage);

            ModelState.AddModelError(PageValues.GenreName, PageValues.DuplicateGenre);
           
            return Page();
        }
    }
}