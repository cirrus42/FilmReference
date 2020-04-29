using FilmReference.DataAccess.DbClasses;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Managers.Interfaces;
using Shared.Models;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Managers
{
    public class StudioPagesManager : IStudioPagesManager
    {
        private readonly IStudioHandler _studioHandler;

        public StudioPagesManager(IStudioHandler studioHandler) =>
            _studioHandler = studioHandler;

        public async Task<bool> SaveStudio(StudioEntity studio)
        {
            if (await _studioHandler.IsDuplicate(studio))
                return false;

            await _studioHandler.SaveStudio(studio);
            return true;
        }

        public async Task<Results<StudioEntity>> GetStudioById(int id) =>
           await _studioHandler.GetStudioById(id);

        public async Task<bool> UpdateStudio(StudioEntity studio)
        {
            if (await _studioHandler.IsDuplicate(studio))
                return false;
            await _studioHandler.UpdateStudio(studio);
            return true;
        }
    }
}