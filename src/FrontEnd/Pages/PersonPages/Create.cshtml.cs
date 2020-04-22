﻿using FilmReference.DataAccess;
using FilmReference.FrontEnd.Helpers;
using FilmReference.FrontEnd.Managers.Interfaces;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.PersonPages
{
    public class CreateModel : PageModel
    {
        private readonly IImageHelper _imageHelper;
        private readonly IPersonPagesManager _personPagesManager;
        public Person Person { get; set; }

        public CreateModel(IImageHelper imageHelper, IPersonPagesManager personPagesManager)
        {
            _imageHelper = imageHelper;
            _personPagesManager = personPagesManager;
        }

        public IActionResult OnGet() =>
            Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var newPerson = new Person();
            var updated = await TryUpdateModelAsync(
                newPerson,
                nameof(Person),
                p => p.PersonId,
                p => p.FirstName,
                p => p.LastName,
                p => p.Description,
                p => p.IsActor,
                p => p.IsDirector,
                p => p.Picture);

            if (!updated) return Page();

            var files = Request.Form.Files;
            if (files.Any())
            {
                var file = files.ElementAt(0);
                if (file.Length > 0)
                {
                    if (!_imageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.PersonPicture, errorMessage);
                        return Page();
                    }

                    _imageHelper.AddImageToEntity(newPerson, file);
                }
            }

            if (await _personPagesManager.SavePerson(newPerson))
            {
                var nextPage = !newPerson.IsActor && newPerson.IsDirector
                    ? PageValues.DirectorIndexPage
                    : PageValues.PersonIndexPage;
                return RedirectToPage(nextPage);
            }

            ModelState.AddModelError(PageValues.PersonFirstName, PageValues.DuplicatePerson);
            ModelState.AddModelError(PageValues.PersonLastName, PageValues.DuplicatePerson);
            return Page();
        }
    }
}