using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Managers.Interfaces
{
    public interface IFilmPagesManager
    {
        Task<Results<FilmDetails>> GetFilmById(int id);
        Task<Results<FilmDetails>> GetFilmWithFilmPerson(int id);
        Task<FilmPagesValues> GetFilmsAndGenres();
        Task<bool> SaveFilm(Film film);
        Task<bool> UpdateFilm(Film film);
        Task RemoveActorsFromFilm(IEnumerable<FilmPerson> filmPersonList);
        Task<FilmPagesValues> GetFilmPageDropDownValues();
    }
}