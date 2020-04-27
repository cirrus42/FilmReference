using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.DataAccess.DbClasses;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IGenrePagesManager
    {
        Task<bool> SaveGenre(GenreEntity genre);
        Task<Results<GenreEntity>> GetGenreById(int id);
        Task<bool> UpdateGenre(GenreEntity genre);
    }
}