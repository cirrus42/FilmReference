using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IStudioHandler
    {
        Task<IEnumerable<Studio>> GetStudios();
        Task<bool> IsDuplicate(Studio studio);
        Task SaveStudio(Studio studio);
        Task<Results<Studio>> GetStudioById(int id);
        Task UpdateStudio(Studio studio);
    }
}