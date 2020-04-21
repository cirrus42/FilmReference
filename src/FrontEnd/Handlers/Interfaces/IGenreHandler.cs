using FilmReference.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IGenreHandler
    {
        Task<IEnumerable<Genre>> GetGenres();
        Task<bool> IsDuplicate(Genre genre);
        Task SaveGenre(Genre genre);
        Task<Results<Genre>> GetGenreById(int id);
        Task UpdateGenre(Genre genre);
    }
}