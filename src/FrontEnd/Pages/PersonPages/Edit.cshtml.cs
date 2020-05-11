using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;

namespace FilmReference.FrontEnd.Pages.PersonPages
{
    public class EditModel : PageModel
    {
        public IImageHelper ImageHelper;
        private readonly IPersonPagesManager _personPagesManager;
        public Person Person { get; set; }

        public EditModel(IImageHelper imageHelper, IPersonPagesManager personPagesManager)
        {
            ImageHelper = imageHelper;
            _personPagesManager = personPagesManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _personPagesManager.GetPersonById(id.Value);

            if (result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            Person = result.Entity;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            if (!ModelState.IsValid)
                return Page();

            var result = await _personPagesManager.GetPersonById(id.Value);

            if (result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            Person = result.Entity;

            var updated = await TryUpdateModelAsync(
                Person,
                nameof(Person),
                p => p.Id,
                p => p.LastName,
                p => p.Description,
                p => p.Actor,
                p => p.Director,
                p => p.Picture);

            if (!updated) return Page();

            var files = Request.Form.Files;
            if (files.Any())
            {
                var file = files.ElementAt(0);
                if (file.Length > 0)
                {
                    if (!ImageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.PersonPicture, errorMessage);
                        return Page();
                    }

                    ImageHelper.AddImageToEntity(result.Entity, file);
                }
            }

            var validationList = (await _personPagesManager.UpdatePerson(Person)).ToList();

            if (validationList.Count == 0)
            {
                var nextPage = !Person.Actor && Person.Director
                    ? PageValues.DirectorIndexPage
                    : PageValues.PersonIndexPage;
                return RedirectToPage(nextPage);
            }

            ModelState.AddModelStateValidation(validationList);

            return Page();
        }
    }
}