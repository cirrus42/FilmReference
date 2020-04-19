using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Managers
{
    public class FilmPagesManager : IFilmPagesManager
    {
        private readonly IPersonHandler _personHandler;
        private readonly IGenreHandler _genreHandler;
        private readonly IStudioHandler _studioHandler;
        private readonly IFilmHandler _filmHandler;
        private readonly IFilmPersonHandler _filmPersonHandler;

        public FilmPagesManager(IPersonHandler personHandler, IGenreHandler genreHandler, IStudioHandler studioHandler, IFilmHandler filmHandler, IFilmPersonHandler filmPersonHandler)
        {
            _personHandler = personHandler;
            _genreHandler = genreHandler;
            _studioHandler = studioHandler;
            _filmHandler = filmHandler;
            _filmPersonHandler = filmPersonHandler;
        }

        public async Task<Results<FilmDetails>> GetFilmById(int id) =>
            await _filmHandler.GetFilmById(id);
        
        public async Task<FilmPagesValues> GetFilmPageDropDownValues()
        {
            var filmPages = new FilmPagesValues(new Genre
            {
                GenreId = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            });

            filmPages.Directors.AddRange((await _personHandler.GetDirectors()).ToList());
            filmPages.Actors = ((await _personHandler.GetActors()).ToList());
            filmPages.Genres.AddRange((await _genreHandler.GetGenres()).ToList());
            filmPages.Studios.AddRange((await _studioHandler.GetStudios()).ToList());

            return filmPages;
        }

        public Task<Results<FilmDetails>> GetFilmWithFilmPerson(int id) => 
            _filmHandler.GetFilmWithFilmPerson(id);

        public async Task<bool> SaveFilm(Film film)  
        {
            if(await _filmHandler.IsDuplicate(film.FilmId, film.Name)) 
                return false;
            
            await _filmHandler.SaveFilm(film);
            return true;
        }

        public async Task RemoveActorsFromFilm(IEnumerable<FilmPerson> filmPersonList)
        {
            foreach (var filmPerson in filmPersonList)
               await _filmPersonHandler.RemoveFilmPerson(filmPerson);
        }

        public async Task<bool> UpdateFilm(Film film)
        {
            if (await _filmHandler.IsDuplicate(film.FilmId, film.Name))
                return false;
            await _filmHandler.UpdateFilm(film);
            return true;
        }

        public async Task<FilmPagesValues> GetFilmsAndGenres()
        {
            var filmPages =
                new FilmPagesValues(new Genre {GenreId = PageValues.Zero, Name = PageValues.All})
                {
                    Films = (await _filmHandler.GetFilms()).ToList()
                };

            filmPages.Genres.AddRange((await _genreHandler.GetGenres()).ToList());
            return filmPages;
        }
    }
}       