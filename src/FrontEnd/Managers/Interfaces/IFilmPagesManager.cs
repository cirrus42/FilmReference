using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Managers.Interfaces
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