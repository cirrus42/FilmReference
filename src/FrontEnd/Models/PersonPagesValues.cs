using System.Collections.Generic;

namespace FilmReference.FrontEnd.Models
{
    public class PersonPagesValues
    {
        public Person Person { get; set; }
        public IEnumerable<Film> Films { get; set; }
    }
}
