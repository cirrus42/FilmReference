using BusinessLogic.Handlers;
using FilmReference.DataAccess.Entities;
using FilmReference.DataAccess.Repositories;
using Moq;
using Xunit;

namespace BusinessLogic.Tests.Handlers
{
    public class FilmPersonHandlerTests
    {
        private readonly Mock<IGenericRepository<FilmPersonEntity>> _filmPersonRepository;
        private readonly FilmPersonHandler _filmPersonHandler;

        public FilmPersonHandlerTests()
        {
            _filmPersonRepository = new Mock<IGenericRepository<FilmPersonEntity>>();
            _filmPersonHandler = new FilmPersonHandler(_filmPersonRepository.Object);
        }

        [Fact]
        public async void RemoveFilmPersonCallsRepository()
        {
            var filmPerson = new FilmPersonEntity();

            await _filmPersonHandler.RemoveFilmPerson(filmPerson);

            _filmPersonRepository.Verify(method => method.Delete(It.IsAny<FilmPersonEntity>()), Times.Once);
            _filmPersonRepository.Verify(method => method.Save(), Times.Once);
        }
    }
}