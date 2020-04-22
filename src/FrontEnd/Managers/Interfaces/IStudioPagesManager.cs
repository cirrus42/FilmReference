using System.Threading.Tasks;
using FilmReference.DataAccess;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IStudioPagesManager
    {
        Task<bool> SaveStudio(Studio studio);
    }
}