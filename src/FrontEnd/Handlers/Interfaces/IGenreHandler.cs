using FilmReference.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IGenreHandler
    {
        Task<IEnumerable<Genre>> GetGenres();
        Task<bool> IsDuplicate(Genre genre);
    }
}