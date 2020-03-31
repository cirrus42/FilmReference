﻿using FilmReference.DataAccess;
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
    public class EditModel : FilmReferencePageModel
    {
        #region Constructor

        public IImageHelper ImageHelper;
        public EditModel(FilmReferenceContext context, IImageHelper imageHelper)
            : base (context)
        {
            ImageHelper = imageHelper;
        }

        #endregion

        #region Properties
        
        public Film Film { get; set; }

        public List<int> SelectedActorIds { get; set; }

        public MultiSelectList SlActor { get; set; }

        public SelectList SlDirector { get; set; }

        public SelectList SlGenre { get; set; }

        public SelectList SlStudio { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Film = await _context.Film
                .Include(f => f.Director)
                .Include(f => f.Genre)
                .Include(f => f.Studio)
                .Include(f => f.FilmPerson)
                .FirstOrDefaultAsync(m => m.FilmId == id);

            if (Film == null)
            {
                return NotFound();
            }

            SelectedActorIds = new List<int>();

            foreach (var filmPerson in Film.FilmPerson)
            {
                SelectedActorIds.Add(filmPerson.PersonId);
            }

            await DoPopulationsAsync();
            return Page();
        }

        #endregion

        #region Post

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var film = _context.Film
                .Include(f => f.FilmPerson)
                .FirstOrDefault(f => f.FilmId == id);
            
            if (film == null)
            {
                return NotFound();
            }

            var selectedActorIds = Request.Form[nameof(SelectedActorIds)];
            var files = Request.Form.Files;

            if (files.Count > 0)
            {
                var file = files[0];
                if (file.Length > 0)
                {
                    if (!ImageHelper.FileTypeOk(file, out var errorMessage))
                    {
                        ModelState.AddModelError(PageValues.FilmPicture, errorMessage);
                        await DoPopulationsAsync();
                        return Page();
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        film.Picture = memoryStream.ToArray();
                    }
                }
            }

            if (await TryUpdateModelAsync(
                film,
                nameof(film),
                f => f.FilmId,
                f => f.Name,
                f => f.Description,
                f => f.Picture,
                f => f.GenreId,
                f => f.DirectorId,
                f => f.StudioId))
            {
                foreach (var filmPerson in film.FilmPerson)
                {
                    _context.FilmPerson.Remove(filmPerson);
                }
                foreach (var actorId in selectedActorIds)
                {
                    film.FilmPerson.Add(new FilmPerson
                    {
                        FilmId = film.FilmId,
                        PersonId = Convert.ToInt32(actorId)
                    });
                }               

                await _context.SaveChangesAsync();
                return RedirectToPage(PageValues.FilmIndexPage);
            }

            await DoPopulationsAsync();
            return Page();

        }

        #endregion

        #region Private Methods

        private async Task DoPopulationsAsync()
        {
            await PopulateActorsAsync();
            await PopulateDirectorsAsync();
            await PopulateGenresAsync();
            await PopulateStudiosAsync();
        }

        private async Task PopulateActorsAsync()
        {
            SlActor = new SelectList(
                                await _context.Person
                                .Include(p => p.FilmPerson)
                                .Where(p => p.IsActor)
                                .OrderBy(p => p.FullName)
                                .ToListAsync(),
                                nameof(Person.PersonId),
                                nameof(Person.FullName));
        }

        private async Task PopulateDirectorsAsync()
        {
            SlDirector = new SelectList(
                                await _context.Person
                                    .Where(p => p.IsDirector)
                                    .OrderBy(p => p.FullName)
                                    .ToListAsync(),
                                nameof(Person.PersonId),
                                nameof(Person.FullName));
        }                                

        private async Task PopulateGenresAsync()
            => SlGenre = new SelectList(
                                await _context.Genre
                                .OrderBy(g => g.Name)
                                .ToListAsync(),
                                nameof(Genre.GenreId),
                                nameof(Genre.Name));

        private async Task PopulateStudiosAsync()
            => SlStudio = new SelectList(
                await _context.Studio
                .OrderBy(s => s.Name)
                .ToListAsync(),
                nameof(Studio.StudioId),
                nameof(Studio.Name));

        #endregion
    }
}
