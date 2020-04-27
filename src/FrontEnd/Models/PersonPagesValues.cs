using System.Collections.Generic;
using FilmReference.DataAccess;
using FilmReference.DataAccess.DbClasses;
using PersonEntity = FilmReference.DataAccess.DbClasses.PersonEntity;

namespace FilmReference.FrontEnd.Models
{
    public class PersonPagesValues
    {
        public PersonEntity Person { get; set; }

        public IEnumerable<FilmEntity> Films { get; set; }
    }
}
