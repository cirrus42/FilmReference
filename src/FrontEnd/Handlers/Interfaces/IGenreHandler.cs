using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IGenreHandler
    {
        Task<IEnumerable<Genre>> GetGenres();
    }
}