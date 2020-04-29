using FilmReference.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    internal interface IFilmHandler
    {
        Task SaveFilm(FilmEntity film);
        Task UpdateFilm(FilmEntity film);
        Task<FilmEntity> GetFilmById(int id);
        Task<FilmEntity> GetFilmWithFilmPerson(int id);
        Task<IEnumerable<FilmEntity>> GetFilms();
        Task<bool> IsDuplicate(int filmId, string filmName);
    }
}