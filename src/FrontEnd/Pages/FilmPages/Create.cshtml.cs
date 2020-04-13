using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Helpers;

namespace FilmReference.FrontEnd.Pages.FilmPages
{
    public class CreateModel : FilmReferencePageModel
    {
        private IImageHelper _imageHelper;
        public CreateModel(FilmReferenceContext context, IImageHelper imageHelper)
            : base (context)
        {
            _imageHelper = imageHelper;
        }

        public Film Film { get; set; }

        public List<int> SelectedActorIds { get; set; }

        public MultiSelectList SlActor { get; set; }

        public SelectList SlDirector { get; set; }

        public SelectList SlGenre { get; set; }

        public SelectList SlStudio { get; set; }


        public async Task<IActionResult> OnGet()
        {
            await DoPopulationsAsync();        
            return Page();
        }


    

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await DoPopulationsAsync();
                return Page();
            }

            var newFilm = new Film(_context)
            {
                FilmPerson = new List<FilmPerson>()
            };

            var selectedActorIds = Request.Form[nameof(SelectedActorIds)];
            var files = Request.Form.Files;
            if (files.Count > 0)
            {
                var file = files[0];
                if (file.Length > 0)
                {
                    if (!_imageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.FilmPicture, errorMessage);
                        await DoPopulationsAsync();
                        return Page();
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        newFilm.Picture = memoryStream.ToArray();
                    }
                }
            }

            if (await TryUpdateModelAsync(
                newFilm,
                nameof(Film),
                f => f.FilmId,
                f => f.Name,
                f => f.Description,
                f => f.Picture,
                f => f.GenreId,
                f => f.DirectorId,
                f => f.StudioId))
            {
                foreach (var actorId in selectedActorIds)
                {
                    newFilm.FilmPerson.Add(new FilmPerson
                    {
                        FilmId = newFilm.FilmId,
                        PersonId = Convert.ToInt32(actorId)
                    });
                }

                _context.Add(newFilm);
                await _context.SaveChangesAsync();
                return RedirectToPage(PageValues.FilmIndexPage);
            }

            await DoPopulationsAsync();
            return Page();
        }

     


        private async Task DoPopulationsAsync()
        {
            await PopulateActorsAsync();
            await PopulateDirectorsAsync();
            await PopulateGenresAsync();
            await PopulateStudiosAsync();
        }

        private async Task PopulateActorsAsync()
        {
            SlActor = new MultiSelectList(
                await _context.Person
                                .Where(p => p.IsActor)
                                .OrderBy(p => p.FullName)
                                .ToListAsync(),
                nameof(Person.PersonId),
                nameof(Person.FullName));
        }

        private async Task PopulateDirectorsAsync()
        {
            var directors = await _context.Person
                .Where(p => p.IsDirector)
                .OrderBy(p => p.FullName)
                .ToListAsync();
            directors.Insert(0, new Person(_context)
            {
                PersonId =  PageValues.MinusOne,
                FullName =  PageValues.PleaseSelect
            });
            SlDirector = new SelectList(
                    directors,
                    nameof(Person.PersonId),
                    nameof(Person.FullName));
        }

        private async Task PopulateGenresAsync()
        {
            var genres = await _context.Genre
                                    .OrderBy(g => g.Name)
                                    .ToListAsync();
            genres.Insert(0, new Genre(_context)
            {
                GenreId = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            });
            SlGenre = new SelectList(
                    genres,
                    nameof(Genre.GenreId),
                    nameof(Genre.Name));
        }

        private async Task PopulateStudiosAsync()
        {
            var studios = await _context.Studio
                                    .OrderBy(s => s.Name)
                                    .ToListAsync();
            studios.Insert(0, new Studio(_context)
            {
                StudioId = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            });
            SlStudio = new SelectList(
                studios,
                nameof(Studio.StudioId),
                nameof(Studio.Name));

        }

 
    }
}
