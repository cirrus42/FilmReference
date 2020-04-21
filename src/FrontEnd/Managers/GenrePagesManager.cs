using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Managers.Interfaces;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Managers
{
    public class GenrePagesManager : IGenrePagesManager
    {
        private readonly IGenreHandler _genreHandler;

        public GenrePagesManager(IGenreHandler genreHandler) => 
            _genreHandler = genreHandler;

        public async Task<bool> SaveGenre(Genre genre)
        {
            if (await _genreHandler.IsDuplicate(genre))
                return false;

            await _genreHandler.SaveGenre(genre);
            return true;
        }

        public async Task<bool> UpdateGenre(Genre genre)
        {
            if (await _genreHandler.IsDuplicate(genre))
                return false;
            await _genreHandler.UpdateGenre(genre);
            return true;
        }

        public Task<Results<Genre>> GetGenreById(int id) =>
            _genreHandler.GetGenreById(id);
    }
}
