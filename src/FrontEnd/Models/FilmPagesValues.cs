using FilmReference.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace FilmReference.FrontEnd.Models
{
    public class FilmPagesValues
    {
        //public Film Film { get; set; }
        public List<int> SelectedActorIds { get; set; }

        public List<Person> Directors { get; set; } = new List<Person>
        {
            new Person
            {
                PersonId = PageValues.MinusOne,
                FullName = PageValues.PleaseSelect
            }
        };

        public List<Person> Actors { get; set; } 

        public List<Genre> Genres { get; set; } = new List<Genre>
        {
            new Genre
            {
                GenreId = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            }
        };

        public List<Studio> Studios { get; set; } = new List<Studio>
        {
            new Studio
            {
                StudioId = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            }
        };

        public SelectList DirectorSelectList() =>
            new SelectList(Directors.ToList(), 
                nameof(Person.PersonId),
                nameof(Person.FullName));

        public MultiSelectList ActorSelectList() =>
            new SelectList(Actors.ToList(),
                nameof(Person.PersonId),
                nameof(Person.FullName));

        public SelectList GenreSelectList() =>
            new SelectList(Genres.ToList(),
                nameof(Genre.GenreId),
                nameof(Genre.Name));
        public SelectList StudioSelectList() =>
            new SelectList(Studios.ToList(),
                nameof(Studio.StudioId),
                nameof(Studio.Name));
    }
}
