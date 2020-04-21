using FilmReference.DataAccess;
using FilmReference.FrontEnd.Helpers;
using FilmReference.FrontEnd.Managers;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.FilmPages
{
    public class CreateModel : PageModel
    {
        private readonly IImageHelper _imageHelper;
        private readonly IFilmPagesManager _filmPagesManager;
        public Film Film { get; set; }
        public List<int> SelectedActorIds { get; set; }
        public FilmPagesValues FilmPagesValues { get; set; }

        public CreateModel( IImageHelper imageHelper, IFilmPagesManager filmPagesManager)
        {
            _imageHelper = imageHelper;
            _filmPagesManager = filmPagesManager;
        }

        public async Task<IActionResult> OnGet()
        {
            FilmPagesValues = await _filmPagesManager.GetFilmPageDropDownValues();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                await OnGet();

            var newFilm = new Film()
            {
                FilmPerson = new List<FilmPerson>()
            };

            var updated = await TryUpdateModelAsync(
                newFilm,
                nameof(Film),
                f => f.FilmId,
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
                    if (!_imageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.FilmPicture, errorMessage);
                        FilmPagesValues = await _filmPagesManager.GetFilmPageDropDownValues();
                        return Page();
                    }

                    _imageHelper.AddImageToEntity(newFilm, file);
                }
            }

            var selectedActorIds = Request.Form[nameof(SelectedActorIds)];

            foreach (var actorId in selectedActorIds)
                newFilm.FilmPerson.Add(
                    new FilmPerson
                    {
                        FilmId = newFilm.FilmId,
                        PersonId = Convert.ToInt32(actorId)
                    });

            if (await _filmPagesManager.SaveFilm(newFilm))
                return RedirectToPage(PageValues.FilmIndexPage);

            ModelState.AddModelError(PageValues.FilmName, PageValues.DuplicatePerson);
            FilmPagesValues = await _filmPagesManager.GetFilmPageDropDownValues();
            return Page();
        }
    }
}