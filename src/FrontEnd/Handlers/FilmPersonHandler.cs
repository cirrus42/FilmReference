using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.DataAccess.DbClasses;
using FilmReference.DataAccess.Repositories;
using FilmReference.FrontEnd.Handlers.Interfaces;

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
