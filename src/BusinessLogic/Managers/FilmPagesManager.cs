using AutoMapper;
using BusinessLogic.Handlers.Interfaces;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;
using FilmReference.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    public class FilmPagesManager : IFilmPagesManager
    {
        private readonly IPersonHandler _personHandler;
        private readonly IGenreHandler _genreHandler;
        private readonly IStudioHandler _studioHandler;
        private readonly IFilmHandler _filmHandler;
        private readonly IFilmPersonHandler _filmPersonHandler;
        private readonly IMapper _mapper;

        public FilmPagesManager(IPersonHandler personHandler,
            IGenreHandler genreHandler, 
            IStudioHandler studioHandler, 
            IFilmHandler filmHandler, 
            IFilmPersonHandler filmPersonHandler, 
            IMapper mapper)
        {
            _personHandler = personHandler;
            _genreHandler = genreHandler;
            _studioHandler = studioHandler;
            _filmHandler = filmHandler;
            _filmPersonHandler = filmPersonHandler;
            _mapper = mapper;
        }

        public async Task<Results<FilmDetails>> GetFilmById(int id)
        {
            var filmEntity = await _filmHandler.GetFilmById(id);
            
            if (filmEntity == null) return new Results<FilmDetails> { HttpStatusCode = HttpStatusCode.NotFound };
            
            return new Results<FilmDetails>
            {
                Entity = new FilmDetails
                {
                    Film = _mapper.Map<Film>(filmEntity),
                    Actors = _mapper.Map<List<Person>>(filmEntity.FilmPerson
                        .Select(filmPerson => filmPerson.Person)
                        .OrderBy(person => person.FullName).ToList())
                },
                HttpStatusCode = HttpStatusCode.OK
            };
        }

        public async Task<Results<FilmDetails>> GetFilmWithFilmPerson(int id)
        {
            var film = await _filmHandler.GetFilmWithFilmPerson(id);

            if (film == null) return new Results<FilmDetails> { HttpStatusCode = HttpStatusCode.NotFound };

            return new Results<FilmDetails>
            {
                Entity = new FilmDetails
                {
                    Film = _mapper.Map<Film>(film)
                },
                HttpStatusCode = HttpStatusCode.OK
            };
        }

        public async Task<FilmPagesValues> GetFilmsAndGenres()
        {
            var filmPages =
                new FilmPagesValues(new Genre { Id = PageValues.Zero, Name = PageValues.All })
                {
                    Films = _mapper.Map<List<Film>>((await _filmHandler.GetFilms()).ToList())
                };

            filmPages.Genres.AddRange(_mapper.Map<List<Genre>>((await _genreHandler.GetGenres()).ToList()));
            return filmPages;
        }

        public async Task<bool> SaveFilm(Film film)
        {
            if (await _filmHandler.IsDuplicate(film.Id, film.Name))
                return false;

            await _filmHandler.SaveFilm(_mapper.Map<FilmEntity>(film));
            return true;
        }

        public async Task<bool> UpdateFilm(Film film)
        {
            if (await _filmHandler.IsDuplicate(film.Id, film.Name))
                return false;
            await _filmHandler.UpdateFilm(_mapper.Map<FilmEntity>(film));
            return true;
        }

        public async Task RemoveActorsFromFilm(IEnumerable<FilmPerson> filmPersonList)
        {
            foreach (var filmPerson in filmPersonList)
                await _filmPersonHandler.RemoveFilmPerson(_mapper.Map<FilmPersonEntity>(filmPerson));
        }

        public async Task<FilmPagesValues> GetFilmPageDropDownValues()
        {
            var filmPages = new FilmPagesValues(new Genre
            {
                Id = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            });

            filmPages.Directors.AddRange(_mapper.Map<List<Person>>((await _personHandler.GetDirectors()).ToList()));
            filmPages.Actors = _mapper.Map<List<Person>>((await _personHandler.GetActors()).ToList());
            filmPages.Genres.AddRange(_mapper.Map<List<Genre>>((await _genreHandler.GetGenres()).ToList()));
            filmPages.Studios.AddRange(_mapper.Map<List<Studio>>((await _studioHandler.GetStudios()).ToList()));

            return filmPages;
        }
    }
}       