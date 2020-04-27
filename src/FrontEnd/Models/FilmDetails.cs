using FilmReference.DataAccess;
using System.Collections.Generic;
using FilmReference.DataAccess.DbClasses;
using PersonEntity = FilmReference.DataAccess.DbClasses.PersonEntity;

namespace FilmReference.FrontEnd.Models
{
    public class FilmDetails
    {
        public FilmEntity Film { get; set; }
        public List<PersonEntity> Actors { get; set; } = new List<PersonEntity>();
    }
}