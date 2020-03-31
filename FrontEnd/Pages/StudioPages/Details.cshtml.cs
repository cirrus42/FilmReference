using FilmReference.DataAccess;
using FilmReference.FrontEnd.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class DetailsModel : FilmReferencePageModel
    {
        #region Constructor

        public DetailsModel(FilmReferenceContext context)
            : base (context)
        {
        }

        #endregion

        #region Properties

        public Studio Studio { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Studio = await _context.Studio.FirstOrDefaultAsync(m => m.StudioId == id);

            if (Studio == null)
            {
                return NotFound();
            }
            return Page();
        }

        #endregion
    }
}
