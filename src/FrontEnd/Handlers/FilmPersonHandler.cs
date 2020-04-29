using FilmReference.DataAccess.Entities;
using FilmReference.DataAccess.Repositories;
using FilmReference.FrontEnd.Handlers.Interfaces;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers
{
    public class FilmPersonHandler : IFilmPersonHandler
    {
        private readonly IGenericRepository<FilmPersonEntity> _filmPersonRepository;

        public FilmPersonHandler(IGenericRepository<FilmPersonEntity> filmPersonRepository) =>
            _filmPersonRepository = filmPersonRepository;

        public async Task RemoveFilmPerson(FilmPersonEntity filmPerson)
        {
            await _filmPersonRepository.Delete(filmPerson);
            await _filmPersonRepository.Save();
        }
           

    }
}
