using FilmReference.DataAccess;
using System.Collections.Generic;
using FilmReference.DataAccess.DbClasses;
using PersonEntity = FilmReference.DataAccess.DbClasses.PersonEntity;

namespace FilmReference.FrontEnd.Models
{
    public class FilmDetails
    {
        public Film Film { get; set; }
        public List<Person> Actors { get; set; } = new List<Person>();
    }
}