using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess.Entities;

namespace BusinessLogic.Handlers.Interfaces
{
    public interface IGenreHandler
    {
        Task<IEnumerable<GenreEntity>> GetGenres();
        Task<bool> IsDuplicate(GenreEntity genre);
        Task SaveGenre(GenreEntity genre);
        Task<GenreEntity> GetGenreById(int id);
        Task UpdateGenre(GenreEntity genre);
    }
}