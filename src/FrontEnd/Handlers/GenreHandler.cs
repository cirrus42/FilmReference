using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Handlers.Interfaces;

namespace FilmReference.FrontEnd.Handlers
{
    public class GenreHandler : IGenreHandler
    {
        private readonly IGenericRepository<Genre> _genreRepository;

        public GenreHandler(IGenericRepository<Genre> personRepository) =>
            _genreRepository = personRepository;

        public async Task<IEnumerable<Genre>> GetGenres() =>
            (await _genreRepository.GetAll()).OrderBy(genre => genre.Name);
    }
}
