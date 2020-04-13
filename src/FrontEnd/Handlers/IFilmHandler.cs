using FilmReference.DataAccess;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers
{
    public interface IFilmHandler
    {
        Task SaveFilm(Film film); 
        Task<bool> IsDuplicate(int filmId, string filmName);
    }
}