using FilmReference.DataAccess.DbClasses;
using System.Threading.Tasks;
using Shared.Models;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IGenrePagesManager
    {
        Task<bool> SaveGenre(GenreEntity genre);
        Task<Results<GenreEntity>> GetGenreById(int id);
        Task<bool> UpdateGenre(GenreEntity genre);
    }
}