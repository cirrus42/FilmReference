using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.DataAccess.DbClasses;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IStudioPagesManager
    {
        Task<bool> SaveStudio(StudioEntity studio);
        Task<Results<StudioEntity>> GetStudioById(int id);
        Task<bool> UpdateStudio(StudioEntity studio);
    }
}