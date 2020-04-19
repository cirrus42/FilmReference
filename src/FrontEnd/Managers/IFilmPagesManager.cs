using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Managers
{
    public interface IFilmPagesManager
    {
        Task<FilmPagesValues> GetFilmPageDropDownValues();
        Task<bool> SaveFilm(Film film);
        Task<Results<FilmDetails>> GetFilmById(int id);
        Task<Results<FilmDetails>> GetFilmWithFilmPerson(int id);
        Task RemoveActorsFromFilm(IEnumerable<FilmPerson> filmPersonList);
        Task<bool> UpdateFilm(Film film);
        Task<FilmPagesValues> GetFilmsAndGenres();
    }
}