﻿using FilmReference.DataAccess.DbClasses;
using FilmReference.FrontEnd.Helpers;
using FilmReference.FrontEnd.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class CreateModel : PageModel
    {
        private readonly IImageHelper _imageHelper;
        private readonly IStudioPagesManager _studioPagesManager;
        public StudioEntity Studio { get; set; }

        public CreateModel(IImageHelper imageHelper, IStudioPagesManager studioPagesManager)
        {
            _imageHelper = imageHelper;
            _studioPagesManager = studioPagesManager;
        }

        public IActionResult OnGet() =>
            Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var newStudio = new StudioEntity();

            var updated = await TryUpdateModelAsync(
                newStudio,
                nameof(Studio),
                s => s.StudioId, s => s.Name, s => s.Description, s => s.Picture);

            if (!updated) return Page();

            var files = Request.Form.Files;

            if (files.Any())
            {
                var file = files.ElementAt(0);
                if (file.Length > 0)
                {
                    if (!_imageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.StudioPicture, errorMessage);
                        return Page();
                    }

                   // _imageHelper.AddImageToEntity(newStudio, file);
                }
            }

            if (await _studioPagesManager.SaveStudio(newStudio))
                return RedirectToPage(PageValues.StudioIndexPage);

            ModelState.AddModelError(PageValues.StudioName, PageValues.DuplicateStudio);
            return Page();
        }
    }
}