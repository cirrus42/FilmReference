using FilmReference.FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IFilmPagesManager
    {
        Task<Results<FilmDetails>> GetFilmById(int id);
        Task<Results<FilmDetails>> GetFilmWithFilmPerson(int id);
        Task<bool> SaveFilm(Film film);
        Task RemoveActorsFromFilm(IEnumerable<FilmPerson> filmPersonList);
        Task<bool> UpdateFilm(Film film);
        Task<FilmPagesValues> GetFilmsAndGenres();
    }
}