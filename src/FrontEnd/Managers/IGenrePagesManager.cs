using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Managers
{
    public interface IGenrePagesManager
    {
        Task<bool> SaveGenre(Genre genre);
        Task<Results<Genre>> GetGenreById(int id);
        Task<bool> UpdateGenre(Genre genre);
    }
}