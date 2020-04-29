using System.Collections.Generic;

namespace Shared.Models
{
    public class FilmDetails
    {
        public Film Film { get; set; }
        public List<Person> Actors { get; set; } = new List<Person>();
    }
}