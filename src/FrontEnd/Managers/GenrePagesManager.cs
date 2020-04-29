using FilmReference.DataAccess.DbClasses;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Managers.Interfaces;
using Shared.Models;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Managers
{
    public class GenrePagesManager : IGenrePagesManager
    {
        private readonly IGenreHandler _genreHandler;

        public GenrePagesManager(IGenreHandler genreHandler) => 
            _genreHandler = genreHandler;

        public async Task<bool> SaveGenre(GenreEntity genre)
        {
            if (await _genreHandler.IsDuplicate(genre))
                return false;

            await _genreHandler.SaveGenre(genre);
            return true;
        }

        public async Task<bool> UpdateGenre(GenreEntity genre)
        {
            if (await _genreHandler.IsDuplicate(genre))
                return false;
            await _genreHandler.UpdateGenre(genre);
            return true;
        }

        public Task<Results<GenreEntity>> GetGenreById(int id) =>
            _genreHandler.GetGenreById(id);
    }
}
