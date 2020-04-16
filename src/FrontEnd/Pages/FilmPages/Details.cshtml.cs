﻿using FilmReference.FrontEnd.Handlers;
using FilmReference.FrontEnd.Helpers;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Handlers.Interfaces;

namespace FilmReference.FrontEnd.Pages.FilmPages
{
    public class DetailsModel : PageModel
    {
        public IImageHelper ImageHelper;
        private readonly IFilmHandler _filmHandler;

        public DetailsModel(IImageHelper imageHelper, IFilmHandler filmHandler)
        {
            ImageHelper = imageHelper;
            _filmHandler = filmHandler;
        }

        public FilmDetails FilmDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _filmHandler.GetFilmById(id.Value);

            if(result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            FilmDetails = result.Entity;

            return Page();
        }
    }
}
