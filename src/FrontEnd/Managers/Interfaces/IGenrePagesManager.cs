using Shared.Models;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IGenrePagesManager
    {
        Task<bool> SaveGenre(Genre genre);
        Task<Results<Genre>> GetGenreById(int id);
        Task<bool> UpdateGenre(Genre genre);
    }
}