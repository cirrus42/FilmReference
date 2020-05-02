using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Managers.Interfaces
{
    public interface IGenrePagesManager
    {
        Task<bool> SaveGenre(Genre genre);
        Task<Results<Genre>> GetGenreById(int id);
        Task<bool> UpdateGenre(Genre genre);
    }
}