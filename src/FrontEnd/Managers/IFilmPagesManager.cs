using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Managers
{
    public interface IFilmPagesManager
    {
        Task<FilmPagesValues> GetFilmPageDropDownValues();
        Task<bool> SaveFilm(Film film);
    }
}