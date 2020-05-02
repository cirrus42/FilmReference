using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessLogic.Helpers;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class EditModel : PageModel
    {
        public readonly IImageHelper ImageHelper;
        private readonly IStudioPagesManager _studioPagesManager;
        public Studio Studio { get; set; }

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
                s => s.Id, s => s.Name, s => s.Description, s => s.Picture);

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

                    ImageHelper.AddImageToEntity(result.Entity, file);
                }
            }

            if (await _studioPagesManager.UpdateStudio(Studio))
                return RedirectToPage(PageValues.StudioIndexPage);

            ModelState.AddModelError(PageValues.StudioName, PageValues.DuplicateStudio);
            return Page();
        }
    }
}
