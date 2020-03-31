using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Helpers;

namespace FilmReference.FrontEnd
{
    public class DetailsModel : FilmReferencePageModel
    {
        public IImageHelper ImageHelper;
        public DetailsModel(FilmReferenceContext context, IImageHelper imageHelper)
            : base (context)
        {
            ImageHelper = imageHelper;
        }

        public Person Person { get; set; }

        public List<Film> Films { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context.Person
                .Include(p => p.FilmPerson)
                    .ThenInclude(fp => fp.Film)
                .FirstOrDefaultAsync(m => m.PersonId == id);

            if (Person == null)
            {
                return NotFound();
            }

            var films = Person.FilmPerson.OrderBy(fp => fp.Film.Name);
            Films = new List<Film>();
            foreach (var film in films)
            {
                Films.Add(film.Film);
            }

            return Page();
        }
    }
}
