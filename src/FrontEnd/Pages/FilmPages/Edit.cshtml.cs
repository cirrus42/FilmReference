using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.FilmPages
{
    public class EditModel : PageModel
    {
        private readonly IFilmPagesManager _filmPagesManager;
        public IImageHelper ImageHelper;
        public Film Film { get; set; }
        public List<int> SelectedActorIds { get; set; }
        public FilmPagesValues FilmPagesValues { get; set; }

        public EditModel(IImageHelper imageHelper, IFilmPagesManager filmPagesManager)
        {
            ImageHelper = imageHelper;
            _filmPagesManager = filmPagesManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _filmPagesManager.GetFilmById(id.Value);

            if (result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            Film = result.Entity.Film;
            SelectedActorIds = result.Entity.Film.FilmPerson.Select(filmPerson => filmPerson.PersonId).ToList();

            FilmPagesValues = await _filmPagesManager.GetFilmPageDropDownValues();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid || id == null)
                return Page();

            var result = await _filmPagesManager.GetFilmWithFilmPerson(id.Value);

            if (result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            var updated = await TryUpdateModelAsync(
                result.Entity.Film,
                nameof(result.Entity.Film),
                f => f.Id,
                f => f.Name,
                f => f.Description,
                f => f.Picture,
                f => f.GenreId,
                f => f.DirectorId,
                f => f.StudioId);

            if (!updated)
            {
               FilmPagesValues = await _filmPagesManager.GetFilmPageDropDownValues();
               return Page();
            }

            var files = Request.Form.Files;

            if (files.Any())
            {
                var file = files.ElementAt(0);
                if (file.Length > 0)
                {
                    if (!ImageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.FilmPicture, errorMessage);
                        FilmPagesValues = await _filmPagesManager.GetFilmPageDropDownValues();
                        return Page();
                    }

                    ImageHelper.AddImageToEntity(result.Entity.Film, file);
                }
            }

            var selectedActorIds = Request.Form[nameof(SelectedActorIds)];

            await _filmPagesManager.RemoveActorsFromFilm(result.Entity.Film.FilmPerson.RemoveItems(selectedActorIds.StingValuesToList()));

            foreach (var personId in selectedActorIds.StingValuesToList())
                result.Entity.Film.FilmPerson.Add(new FilmPerson { FilmId = result.Entity.Film.Id, PersonId = personId });

            if (await _filmPagesManager.UpdateFilm(result.Entity.Film))
                return RedirectToPage(PageValues.FilmIndexPage);

            ModelState.AddModelError(PageValues.FilmName, PageValues.DuplicatePerson);
            FilmPagesValues = await _filmPagesManager.GetFilmPageDropDownValues();
            return Page();
        }
    }
}