using FilmReference.DataAccess.DbClasses;
using Shared.Models;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IStudioPagesManager
    {
        Task<bool> SaveStudio(StudioEntity studio);
        Task<Results<StudioEntity>> GetStudioById(int id);
        Task<bool> UpdateStudio(StudioEntity studio);
    }
}