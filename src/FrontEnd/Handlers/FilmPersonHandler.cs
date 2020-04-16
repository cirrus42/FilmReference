using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using FilmReference.FrontEnd.Handlers.Interfaces;

namespace FilmReference.FrontEnd.Handlers
{
    public class FilmPersonHandler : IFilmPersonHandler
    {
        private readonly IGenericRepository<FilmPerson> _filmPersonRepository;

        public FilmPersonHandler(IGenericRepository<FilmPerson> filmPersonRepository) =>
            _filmPersonRepository = filmPersonRepository;

        public async Task RemoveFilmPerson(FilmPerson filmPerson)
        {
            await _filmPersonRepository.Delete(filmPerson);
            await _filmPersonRepository.Save();
        }
           

    }
}
