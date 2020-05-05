using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess.Entities;

namespace BusinessLogic.Handlers.Interfaces
{
    public interface IStudioHandler
    {
        Task<IEnumerable<StudioEntity>> GetStudios();
        Task<bool> IsDuplicate(StudioEntity studio);
        Task SaveStudio(StudioEntity studio);
        Task UpdateStudio(StudioEntity studio);
        Task<StudioEntity> GetStudioById(int id);
    }
}