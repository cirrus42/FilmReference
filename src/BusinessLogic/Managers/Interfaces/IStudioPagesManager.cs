using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Managers.Interfaces
{
    public interface IStudioPagesManager
    {
        Task<bool> SaveStudio(Studio studio);
        Task<bool> UpdateStudio(Studio studio);
        Task<Results<Studio>> GetStudioById(int id);
        Task<IEnumerable<Studio>> GetStudios();
    }
}