using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IStudioPagesManager
    {
        Task<bool> SaveStudio(Studio studio);
        Task<Results<Studio>> GetStudioById(int id);
        Task<bool> UpdateStudio(Studio studio);
    }
}