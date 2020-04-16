using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IFilmHandler
    {
        Task SaveFilm(Film film); 
        Task<bool> IsDuplicate(int filmId, string filmName);
        Task<Results<FilmDetails>> GetFilmById(int id);
        Task<Results<FilmDetails>> GetFilmWithFilmPerson(int id);
        Task UpdateFilm(Film film);
    }
}