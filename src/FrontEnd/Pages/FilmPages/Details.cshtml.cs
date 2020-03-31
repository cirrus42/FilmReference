using FilmReference.DataAccess;
using FilmReference.FrontEnd.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Pages.FilmPages
{
    public class DetailsModel : FilmReferencePageModel
    {
        #region Constructor

        public DetailsModel(FilmReferenceContext context)
            : base (context)
        {
        }

        #endregion
        #region Properties

        public Film Film { get; set; }

        public List<Person> Actors { get; set; }

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
                    .ThenInclude(fp => fp.Person)
                .FirstOrDefaultAsync(m => m.FilmId == id);

            if (Film == null)
            {
                return NotFound();
            }

            var actors = Film.FilmPerson.OrderBy(fp => fp.Person.FullName).ToList();
            Actors = new List<Person>();
            foreach (var actor in actors)
            {
                Actors.Add(actor.Person);
            }

            return Page();
        }

        #endregion
    }
}
