using FilmReference.DataAccess;
using FilmReference.FrontEnd.Helpers;
using FilmReference.FrontEnd.Managers;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.FilmPages
{
    public class EditModel : FilmReferencePageModel
    {
        private IFilmPagesManager _filmPagesManager;
        public IImageHelper ImageHelper;
        public EditModel(FilmReferenceContext context, IImageHelper imageHelper, IFilmPagesManager filmPagesManager)
            : base (context)
        {
            ImageHelper = imageHelper;
            _filmPagesManager = filmPagesManager;
        }

        public Film Film { get; set; }
        public List<int> SelectedActorIds { get; set; }
        public FilmPagesValues FilmPagesValues { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _filmPagesManager.GetFilmById(id.Value);

            if(result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            Film = result.Entity.Film; 
            SelectedActorIds = result.Entity.Film.FilmPerson.Select(filmPerson => filmPerson.PersonId).ToList();

            FilmPagesValues = await _filmPagesManager.GetFilmPageDropDownValues();
            return Page();
        }



   

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _filmPagesManager.GetFilmWithFilmPerson(id.Value);

            if(result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            var film = result.Entity.Film;

            var updated = await TryUpdateModelAsync(
                film,
                nameof(film),
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

            ////var film = _context.Film
            ////    .Include(f => f.FilmPerson)
            ////    .FirstOrDefault(f => f.FilmId == id);

            //if (film == null)
            //{
            //    return NotFound();
            //}

            //var selectedActorIds = Request.Form[nameof(SelectedActorIds)];
            //var files = Request.Form.Files;

            //if (files.Count > 0)
            //{
            //    var file = files[0];
            //    if (file.Length > 0)
            //    {
            //        if (!ImageHelper.FileTypeOk(file, out var errorMessage))
            //        {
            //            ModelState.AddModelError(PageValues.FilmPicture, errorMessage);
            //            FilmPagesValues = await _filmPagesManager.GetFilmPageDropDownValues();
            //            return Page();
            //        }

            //        using (var memoryStream = new MemoryStream())
            //        {
            //            file.CopyTo(memoryStream);
            //            film.Picture = memoryStream.ToArray();
            //        }
            //    }
            //}

            //if (await TryUpdateModelAsync(
            //    film,
            //    nameof(film),
            //    f => f.FilmId,
            //    f => f.Name,
            //    f => f.Description,
            //    f => f.Picture,
            //    f => f.GenreId,
            //    f => f.DirectorId,
            //    f => f.StudioId))
            //{
            //    foreach (var filmPerson in film.FilmPerson)
            //    {
            //        _context.FilmPerson.Remove(filmPerson);
            //    }
            //    foreach (var actorId in selectedActorIds)
            //    {
            //        film.FilmPerson.Add(new FilmPerson
            //        {
            //            FilmId = film.FilmId,
            //            PersonId = Convert.ToInt32(actorId)
            //        });
            //    }               

            //    await _context.SaveChangesAsync();
            //    return RedirectToPage(PageValues.FilmIndexPage);
            //}

            //FilmPagesValues = await _filmPagesManager.GetFilmPageDropDownValues();
            //return Page();

        }





        //private async Task DoPopulationsAsync()
        //{
        //    await PopulateActorsAsync();
        //    await PopulateDirectorsAsync();
        //    await PopulateGenresAsync();
        //    await PopulateStudiosAsync();
        //}

        //private async Task PopulateActorsAsync()
        //{
        //    SlActor = new SelectList(
        //                        await _context.Person
        //                        .Include(p => p.FilmPerson)
        //                        .Where(p => p.IsActor)
        //                        .OrderBy(p => p.FullName)
        //                        .ToListAsync(),
        //                        nameof(Person.PersonId),
        //                        nameof(Person.FullName));
        //}

        //private async Task PopulateDirectorsAsync()
        //{
        //    SlDirector = new SelectList(
        //                        await _context.Person
        //                            .Where(p => p.IsDirector)
        //                            .OrderBy(p => p.FullName)
        //                            .ToListAsync(),
        //                        nameof(Person.PersonId),
        //                        nameof(Person.FullName));
        //}                                

        //private async Task PopulateGenresAsync()
        //    => SlGenre = new SelectList(
        //                        await _context.Genre
        //                        .OrderBy(g => g.Name)
        //                        .ToListAsync(),
        //                        nameof(Genre.GenreId),
        //                        nameof(Genre.Name));

        //private async Task PopulateStudiosAsync()
        //    => SlStudio = new SelectList(
        //        await _context.Studio
        //        .OrderBy(s => s.Name)
        //        .ToListAsync(),
        //        nameof(Studio.StudioId),
        //        nameof(Studio.Name));


    }
}
