using FilmReference.DataAccess;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Handlers
{
    public interface IFilmHandler
    {
        Task SaveFilm(Film film); 
        Task<bool> IsDuplicate(int filmId, string filmName);
        Task<Results<FilmDetails>> GetFilmById(int id);
    }
}