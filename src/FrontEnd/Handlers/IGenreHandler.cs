using FilmReference.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers
{
    public interface IGenreHandler
    {
        Task<IEnumerable<Genre>> GetGenres();
    }
}