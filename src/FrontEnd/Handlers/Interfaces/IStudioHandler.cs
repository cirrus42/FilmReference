using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IStudioHandler
    {
        Task<IEnumerable<Studio>> GetStudios();
        Task<bool> IsDuplicate(Studio studio);
        Task SaveStudio(Studio studio);
    }
}