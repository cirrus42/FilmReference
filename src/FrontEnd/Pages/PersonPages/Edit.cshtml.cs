using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Helpers;

namespace FilmReference.FrontEnd
{
    public class EditModel : FilmReferencePageModel
    {
        #region Constructor

        public IImageHelper ImageHelper;
        public EditModel(FilmReferenceContext context, IImageHelper imageHelper)
            : base (context)
        {
            ImageHelper = imageHelper;
        }

        #endregion

        #region Properties

        public Person Person { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context.Person.FirstOrDefaultAsync(m => m.PersonId == id);

            if (Person == null)
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

            Person = await _context.Person.FirstOrDefaultAsync(p => p.PersonId == id);

            if (Person == null)
            {
                return NotFound();
            }

            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                var file = files[0];
                if (file.Length > 0)
                {
                    if (!ImageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.PersonPicture, errorMessage);
                        return Page();
                    }
                }

                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    Person.Picture = memoryStream.ToArray();
                }
            }

            if (await TryUpdateModelAsync(
                Person,
                nameof(Person),
                p => p.PersonId, p => p.FirstName, p => p.LastName, p => p.Description, p => p.IsActor, p => p.IsDirector, p => p.Picture))
            {
                await _context.SaveChangesAsync();
                var nextPage = !Person.IsActor && Person.IsDirector
                    ? PageValues.DirectorIndexPage
                    : PageValues.PersonIndexPage;
                return RedirectToPage(nextPage);
            }

            return Page();
        }

        #endregion
    }
}
