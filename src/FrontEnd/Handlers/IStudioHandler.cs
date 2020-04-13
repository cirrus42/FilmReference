using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess;

namespace FilmReference.FrontEnd.Handlers
{
    public interface IStudioHandler
    {
        Task<IEnumerable<Studio>> GetStudios();
    }
}