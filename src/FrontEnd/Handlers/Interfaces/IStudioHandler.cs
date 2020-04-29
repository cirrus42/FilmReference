using FilmReference.DataAccess.DbClasses;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IStudioHandler
    {
        Task<IEnumerable<StudioEntity>> GetStudios();
        Task<bool> IsDuplicate(StudioEntity studio);
        Task SaveStudio(StudioEntity studio);
        Task<Results<StudioEntity>> GetStudioById(int id);
        Task UpdateStudio(StudioEntity studio);
    }
}