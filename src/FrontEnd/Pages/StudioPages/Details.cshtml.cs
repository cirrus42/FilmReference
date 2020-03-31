using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Helpers;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class DetailsModel : FilmReferencePageModel
    {
        #region Constructor

        public IImageHelper ImageHelper;
        public DetailsModel(FilmReferenceContext context, IImageHelper imageHelper)
            : base (context)
        {
            ImageHelper = imageHelper;
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
