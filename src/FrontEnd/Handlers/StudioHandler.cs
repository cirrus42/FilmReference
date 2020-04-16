using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Handlers.Interfaces;

namespace FilmReference.FrontEnd.Handlers
{
    public class StudioHandler : IStudioHandler
    {
        private readonly IGenericRepository<Studio> _genreRepository;

        public StudioHandler(IGenericRepository<Studio> personRepository) =>
            _genreRepository = personRepository;

        public async Task<IEnumerable<Studio>> GetStudios() =>
            (await _genreRepository.GetAll()).OrderBy(genre => genre.Name);
    }
}