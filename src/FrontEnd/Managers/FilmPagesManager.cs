using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers;
using FilmReference.FrontEnd.Models;
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

        public FilmPagesManager(IPersonHandler personHandler, IGenreHandler genreHandler, IStudioHandler studioHandler, IFilmHandler filmHandler)
        {
            _personHandler = personHandler;
            _genreHandler = genreHandler;
            _studioHandler = studioHandler;
            _filmHandler = filmHandler;
        }

        public async Task<Results<FilmDetails>> GetFilmById(int id) =>
            await _filmHandler.GetFilmById(id);
        
        public async Task<FilmPagesValues> GetFilmPageDropDownValues()
        {
            var filmPages = new FilmPagesValues();

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
    }
}


        