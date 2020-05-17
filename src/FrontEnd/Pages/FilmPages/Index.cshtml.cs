using BusinessLogic.Helpers;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.FilmPages
{
    public class IndexModel : PageModel
    {
        private readonly IFilmPagesManager _filmPagesManager;
        public IImageHelper ImageHelper;
        public int GenreId { get; set; }
        public FilmPagesValues FilmPagesValues;

        public IndexModel( IImageHelper imageHelper, IFilmPagesManager filmPagesManager)
        {
            ImageHelper = imageHelper;
            _filmPagesManager = filmPagesManager;
            GenreId = 0;
        }
        public async Task OnGetAsync() => FilmPagesValues = 
            await _filmPagesManager.GetFilmsAndGenres();
        
        public async Task OnPostAsync()
        {
            FilmPagesValues = await _filmPagesManager.GetFilmsAndGenres();
            if (Request.HasFormContentType) GenreId = Convert.ToInt32(Request.Form["GenreId"]);
            if (GenreId > 0) FilmPagesValues.Films = FilmPagesValues.Films.Where(film => film.GenreId == GenreId).ToList();
        }
    }
}