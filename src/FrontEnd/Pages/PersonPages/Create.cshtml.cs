using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Helpers;

namespace FilmReference.FrontEnd
{
    public class CreateModel : FilmReferencePageModel
    {
        private IImageHelper _imageHelper;
        public CreateModel(FilmReferenceContext context, IImageHelper imageHelper)
            : base (context)
        {
            _imageHelper = imageHelper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        
        public Person Person { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newPerson = new Person(_context);

            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                var file = files[0];
                if (file.Length > 0)
                {
                    if (!_imageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.PersonPicture, errorMessage);
                        return Page();
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        newPerson.Picture = memoryStream.ToArray();
                    }
                }
            }

            if (await TryUpdateModelAsync(
                newPerson,
                nameof(Person),
                    p => p.PersonId,
                    p => p.FirstName,
                    p => p.LastName,
                    p => p.Description,
                    p => p.IsActor,
                    p => p.IsDirector,
                    p => p.Picture))
            {
                _context.Add(newPerson);
                await _context.SaveChangesAsync();
                var nextPage = !newPerson.IsActor && newPerson.IsDirector
                    ? PageValues.DirectorIndexPage
                    : PageValues.PersonIndexPage;
                return RedirectToPage(nextPage);
            }
            return Page();
        }
    }
}
