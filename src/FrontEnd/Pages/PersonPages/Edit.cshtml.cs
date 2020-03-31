using FilmReference.DataAccess;
using FilmReference.FrontEnd.Classes;
using FilmReference.FrontEnd.Classes.Helpers;
using FilmReference.FrontEnd.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd
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
                        ModelState.AddModelError(ConfigValues.StringValues.PersonPicture, errorMessage);
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
                    ? ConfigValues.StringValues.DirectorIndexPage
                    : ConfigValues.StringValues.PersonIndexPage;
                return RedirectToPage(nextPage);
            }

            return Page();
        }

        #endregion
    }
}
