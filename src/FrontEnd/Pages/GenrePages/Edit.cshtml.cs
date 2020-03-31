using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.GenrePages
{
    public class EditModel : FilmReferencePageModel
    {
        #region Constructor

        public EditModel(FilmReferenceContext context)
            : base (context)
        {
        }

        #endregion

        #region Properties

        public Genre Genre { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genre = await _context.Genre.FirstOrDefaultAsync(m => m.GenreId == id);

            if (Genre == null)
            {
                return NotFound();
            }
            return Page();
        }

        #endregion

        #region Post

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Genre = await _context.Genre.FirstOrDefaultAsync(g => g.GenreId == id);

            if (Genre == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                Genre,
                nameof(Genre),
                g => g.GenreId, g => g.Name, g => g.Description))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage(PageValues.GenreIndexPage);
            }

            return Page();
        }

        #endregion
    }
}
