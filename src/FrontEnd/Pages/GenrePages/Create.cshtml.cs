using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.GenrePages
{
    public class CreateModel : FilmReferencePageModel
    {

        public CreateModel(FilmReferenceContext context)
            : base (context)
        {
        }

        public Genre Genre { get; set; }

        public IActionResult OnGet() => 
            Page();
        

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            
            var newGenre = new Genre(_context);

            if (await TryUpdateModelAsync(
                newGenre,
                nameof(Genre),
                g => g.GenreId, g => g.Name, g => g.Description))
            {
                _context.Add(newGenre);
                await _context.SaveChangesAsync();
                return RedirectToPage(PageValues.GenreIndexPage);
            }

            return Page();
        }

    }
}
