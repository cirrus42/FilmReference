using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.DataAccess.DbClasses;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IFilmHandler
    {
        Task SaveFilm(FilmEntity film); 
        Task<bool> IsDuplicate(int filmId, string filmName);
        Task<Results<FilmDetails>> GetFilmById(int id);
        Task<Results<FilmDetails>> GetFilmWithFilmPerson(int id);
        Task UpdateFilm(FilmEntity film);
        Task<IEnumerable<FilmEntity>> GetFilms();
    }
}