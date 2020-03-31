using FilmReference.DataAccess;
using FilmReference.FrontEnd.Classes;
using FilmReference.FrontEnd.Config;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.FilmPages
{
    public class IndexModel : FilmReferencePageModel
    {
        #region Constructor

        public IndexModel(FilmReferenceContext context)
            : base (context)
        {
        }

        #endregion

        #region Properties

        public IList<Film> Film { get;set; }

        public IList<Genre> Genre { get; set; }

        public int GenreId { get; set; }

        #endregion

        #region Get

        public async Task OnGetAsync()
        {
            GenreId = 0;
            await DoPopulations();
        }

        #endregion

        #region Post

        public async Task OnPostAsync()
        {
            await DoPopulations();
        }

        #endregion

        #region Private Methods

        private async Task DoPopulations()
        {
            Film = await _context.Film
                .OrderBy(f => f.Name)
                .ToListAsync();

            Genre = await _context.Genre
                .OrderBy(g => g.Name)
                .ToListAsync();
            Genre.Insert(0, new Genre(_context)
            {
                GenreId = ConfigValues.All.Int,
                Name = ConfigValues.All.Text
            });

            try
            {
                var genreId = 0;
                if (Request.HasFormContentType)
                {
                    genreId = Convert.ToInt32(Request.Form["GenreId"]);
                }
                if (genreId > 0)
                {
                    Film = Film.Where(f => f.GenreId == genreId).ToList();
                }
                GenreId = genreId;
            }
            catch { }

        }

        #endregion
    }
}
