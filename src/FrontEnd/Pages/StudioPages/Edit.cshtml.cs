using FilmReference.DataAccess;
using FilmReference.FrontEnd.Helpers;
using FilmReference.FrontEnd.Managers.Interfaces;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FilmReference.DataAccess.DbClasses;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class EditModel : PageModel
    {
        public readonly IImageHelper ImageHelper;
        private IStudioPagesManager _studioPagesManager;
        public StudioEntity Studio { get; set; }

        public EditModel( IImageHelper imageHelper, IStudioPagesManager studioPagesManager)
        {
            ImageHelper = imageHelper;
            _studioPagesManager = studioPagesManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var results = await _studioPagesManager.GetStudioById(id.Value);

            if (results.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            Studio = results.Entity;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();
            
            if (!ModelState.IsValid)
                return Page();

            var result = await _studioPagesManager.GetStudioById(id.Value);

            if (result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            var updated = await TryUpdateModelAsync(
                Studio,
                nameof(Studio),
                s => s.StudioId, s => s.Name, s => s.Description, s => s.Picture);

            if(!updated)  return Page();

            var files = Request.Form.Files;

            if (files.Any())
            {
                var file = files.ElementAt(0);
                if (file.Length > 0)
                {
                    if (!ImageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.StudioPicture, errorMessage);
                        return Page();
                    }

                    await using var memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);
                    result.Entity.Picture = memoryStream.ToArray();
                }
            }

            if (await _studioPagesManager.UpdateStudio(Studio))
                return RedirectToPage(PageValues.StudioIndexPage);

            ModelState.AddModelError(PageValues.StudioName, PageValues.DuplicateStudio);
            return Page();
        }
    }
}
