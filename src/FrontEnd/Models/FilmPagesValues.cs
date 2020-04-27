using FilmReference.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using FilmReference.DataAccess.DbClasses;
using PersonEntity = FilmReference.DataAccess.DbClasses.PersonEntity;

namespace FilmReference.FrontEnd.Models
{
    public class FilmPagesValues
    {
        public FilmPagesValues(GenreEntity genre) =>
            Genres = new List<GenreEntity> {genre};
        
        public List<FilmEntity> Films { get; set; }

        public List<PersonEntity> Directors { get; set; } = new List<PersonEntity>
        {
            new PersonEntity
            {
                PersonId = PageValues.MinusOne,
                FullName = PageValues.PleaseSelect
            }
        };

        public List<PersonEntity> Actors { get; set; } 

        public List<GenreEntity> Genres { get; set; }

        public List<StudioEntity> Studios { get; set; } = new List<StudioEntity>
        {
            new StudioEntity
            {
                StudioId = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            }
        };

        public SelectList DirectorSelectList() =>
            new SelectList(Directors.ToList(), 
                nameof(PersonEntity.PersonId),
                nameof(PersonEntity.FullName));

        public MultiSelectList ActorSelectList() =>
            new SelectList(Actors.ToList(),
                nameof(PersonEntity.PersonId),
                nameof(PersonEntity.FullName));

        public SelectList GenreSelectList() =>
            new SelectList(Genres.ToList(),
                nameof(GenreEntity.GenreId),
                nameof(GenreEntity.Name));
        public SelectList StudioSelectList() =>
            new SelectList(Studios.ToList(),
                nameof(StudioEntity.StudioId),
                nameof(StudioEntity.Name));
    }
}
