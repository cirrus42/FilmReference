using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Helpers;

namespace FilmReference.FrontEnd
{
    public class IndexModel : FilmReferencePageModel
    {
        public IImageHelper ImageHelper;
        public IndexModel(FilmReferenceContext context, IImageHelper imageHelper)
            : base (context)
        {
            ImageHelper = imageHelper;
        }

        public List<Person> Person { get;set; }

        public List<string> AtoZ { get; set; }

        public async Task OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = "A";
            }
            Person = await _context.Person
                .Include(p => p.FilmPerson)
                .Where(p =>
                    p.IsActor &&
                    p.FullName.StartsWith(id))
                .OrderBy(p => p.FullName)
                .ToListAsync();

            AtoZ = new List<string>();

            for (var i = 65; i <= 90; i ++)
            {
                AtoZ.Add(((char)i).ToString());
            }
        }
    }
}
