using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.DataAccess.DbClasses;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IFilmPagesManager
    {
        Task<FilmPagesValues> GetFilmPageDropDownValues();
        Task<bool> SaveFilm(FilmEntity film);
        Task<Results<FilmDetails>> GetFilmById(int id);
        Task<Results<FilmDetails>> GetFilmWithFilmPerson(int id);
        Task RemoveActorsFromFilm(IEnumerable<FilmPersonEntity> filmPersonList);
        Task<bool> UpdateFilm(FilmEntity film);
        Task<FilmPagesValues> GetFilmsAndGenres();
    }
}