using FilmReference.DataAccess.DbClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IFilmHandler
    {
        Task SaveFilm(FilmEntity film); 
        Task<bool> IsDuplicate(int filmId, string filmName);
        Task<FilmEntity> GetFilmById(int id);
        Task<FilmEntity> GetFilmWithFilmPerson(int id);
        Task UpdateFilm(FilmEntity film);
        Task<IEnumerable<FilmEntity>> GetFilms();
    }
}