using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Managers.Interfaces;

namespace FilmReference.FrontEnd.Managers
{
    public class StudioPagesManager : IStudioPagesManager
    {
        private readonly IStudioHandler _studioHandler;

        public StudioPagesManager(IStudioHandler studioHandler) =>
            _studioHandler = studioHandler;

        public async Task<bool> SaveStudio(Studio studio)
        {
            if (await _studioHandler.IsDuplicate(studio))
                return false;

            await _studioHandler.SaveStudio(studio);
            return true;
        }
    }
}
