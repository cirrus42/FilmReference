using System.Collections.Generic;
using FilmReference.DataAccess;

namespace FilmReference.FrontEnd.Models
{
    public class PersonPagesValues
    {
        public Person Person { get; set; }

        public IEnumerable<Film> Films { get; set; }
    }
}
