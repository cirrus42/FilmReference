using AutoMapper;
using BusinessLogic.Handlers.Interfaces;
using BusinessLogic.Managers;
using BusinessLogic.Models;
using FilmReference.DataAccess.Entities;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogic.Tests.Managers
{
    public class FilmPagesManagerTests
    {
        private readonly Mock<IPersonHandler> _personHandler;
        private readonly Mock<IGenreHandler> _genreHandler;
        private readonly Mock<IStudioHandler> _studioHandler;
        private readonly Mock<IFilmHandler> _filmHandler;
        private readonly Mock<IFilmPersonHandler> _filmPersonHandler;
        private readonly Mock<IMapper> _mapper;
        private readonly FilmPagesManager _filmPagesManager;

        public FilmPagesManagerTests()
        {
            _mapper = new Mock<IMapper>();
            _personHandler = new Mock<IPersonHandler>();
            _genreHandler = new Mock<IGenreHandler>();
            _studioHandler = new Mock<IStudioHandler>();
            _filmHandler = new Mock<IFilmHandler>();
            _filmPersonHandler = new Mock<IFilmPersonHandler>();

            _filmPagesManager = new FilmPagesManager(_personHandler.Object, _genreHandler.Object, _studioHandler.Object, _filmHandler.Object, _filmPersonHandler.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetFilmByIdReturnsFilm()
        {
            var personEntity = new PersonEntity { FirstName = "Test", LastName = "Surname" };
            var filmPersonEntity = new FilmPersonEntity { Person = personEntity };
            var personEntityList = new Collection<FilmPersonEntity> { filmPersonEntity };

            var filmEntity = new FilmEntity { Name = "Film", FilmPerson = personEntityList };
            var film = new Film { Name = "Film" };

            var person = new Person { FirstName = "Test", LastName = "Surname" };
            var actors = new List<Person> { person };

            _filmHandler.Setup(method => method.GetFilmById(It.IsAny<int>())).ReturnsAsync(filmEntity);

            _mapper.Setup(method => method.Map<Film>(It.IsAny<FilmEntity>())).Returns(film);
            _mapper.Setup(method => method.Map<List<Person>>(It.IsAny<List<PersonEntity>>())).Returns(actors);

            var methodResult = await _filmPagesManager.GetFilmById(1);

            _filmHandler.Verify(method => method.GetFilmById(It.IsAny<int>()), Times.Once);
            _mapper.Verify(method => method.Map<Film>(It.IsAny<FilmEntity>()), Times.Once);
            _mapper.Verify(method => method.Map<List<Person>>(It.IsAny<List<PersonEntity>>()), Times.Once);

            methodResult.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            var filmDetails = methodResult.Entity;

            filmDetails.Film.Should().Be(film);
            filmDetails.Actors.Should().Contain(person);
        }

        [Fact]
        public async Task GetFilmByIdReturnsNullReturnsNotFound()
        {
            _filmHandler.Setup(method => method.GetFilmById(It.IsAny<int>())).ReturnsAsync((FilmEntity)null);

            var methodResult = await _filmPagesManager.GetFilmById(1);

            _filmHandler.Verify(method => method.GetFilmById(It.IsAny<int>()), Times.Once);
            _mapper.Verify(method => method.Map<Film>(It.IsAny<FilmEntity>()), Times.Never);
            _mapper.Verify(method => method.Map<List<Person>>(It.IsAny<List<PersonEntity>>()), Times.Never);

            methodResult.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetFilmPageDropDownValuesReturnsFilmPagesValues()
        {
            var directorEntity = new PersonEntity { FirstName = "Steven", LastName = "Speelberger" };
            var directorList = new List<PersonEntity> { directorEntity };

            var director = new Person { FirstName = "Steven", LastName = "Speelberger" };
            var directors = new List<Person> { director };

            var actorEntity = new PersonEntity { FirstName = "Ed", LastName = "Wood" };
            var actorList = new List<PersonEntity> { actorEntity };

            var actor = new Person { FirstName = "Ed", LastName = "Wood" };
            var actors = new List<Person> { actor };

            var genreEntity = new GenreEntity { Name = "A New Genre" };
            var genresList = new List<GenreEntity> { genreEntity };

            var genre = new Genre { Name = "A New Genre" };
            var genres = new List<Genre> { genre };

            var studioEntity = new StudioEntity { Name = "Studio" };
            var studiosList = new List<StudioEntity> { studioEntity };

            var studio = new Studio { Name = "Studio" };
            var studios = new List<Studio> { studio };

            _personHandler.Setup(method => method.GetDirectors()).ReturnsAsync(directorList);
            _personHandler.Setup(method => method.GetActors()).ReturnsAsync(actorList);
            _genreHandler.Setup(method => method.GetGenres()).ReturnsAsync(genresList);
            _studioHandler.Setup(method => method.GetStudios()).ReturnsAsync(studiosList);

            _mapper.SetupSequence(method => method.Map<List<Person>>(It.IsAny<List<PersonEntity>>())).Returns(directors).Returns(actors);
            _mapper.Setup(method => method.Map<List<Genre>>(It.IsAny<List<GenreEntity>>())).Returns(genres);
            _mapper.Setup(method => method.Map<List<Studio>>(It.IsAny<List<StudioEntity>>())).Returns(studios);

            var output = await _filmPagesManager.GetFilmPageDropDownValues();

            _personHandler.Verify(method => method.GetDirectors(), Times.Once);
            _personHandler.Verify(method => method.GetActors(), Times.Once);
            _genreHandler.Verify(method => method.GetGenres(), Times.Once);
            _studioHandler.Verify(method => method.GetStudios(), Times.Once);

            _mapper.Verify(method => method.Map<List<Person>>(It.IsAny<List<PersonEntity>>()), Times.Exactly(2));
            _mapper.Verify(method => method.Map<List<Genre>>(It.IsAny<List<GenreEntity>>()), Times.Once);
            _mapper.Verify(method => method.Map<List<Studio>>(It.IsAny<List<StudioEntity>>()), Times.Once);

            output.Directors.Count.Should().Be(2);
            output.Actors.Count.Should().Be(1);
            output.Genres.Count.Should().Be(2);
            output.Studios.Count.Should().Be(2);

            output.Directors.Should().Contain(directors);
            output.Actors.Should().Contain(actors);
            output.Genres.Should().Contain(genre);
            output.Studios.Should().Contain(studio);

            output.Directors.Should().ContainEquivalentOf(new Person
            {
                Id = PageValues.MinusOne,
                FullName = PageValues.PleaseSelect
            });

            output.Genres.Should().ContainEquivalentOf(new Genre
            {
                Id = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            });

            output.Studios.Should().ContainEquivalentOf(new Studio
            {
                Id = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            });
        }

        [Fact]
        public async Task GetFilmWithFilmPersonReturnsFilmValues()
        {
            var filmEntity = new FilmEntity { Name = "Film" };
            var film = new Film { Name = "Film" };

            _filmHandler.Setup(method => method.GetFilmWithFilmPerson(It.IsAny<int>())).ReturnsAsync(filmEntity);
            _mapper.Setup(method => method.Map<Film>(It.IsAny<FilmEntity>())).Returns(film);

            var output = await _filmPagesManager.GetFilmWithFilmPerson(2);

            _filmHandler.Verify(method => method.GetFilmWithFilmPerson(It.IsAny<int>()), Times.Once);
            _mapper.Verify(method => method.Map<Film>(It.IsAny<FilmEntity>()), Times.Once);

            output.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            var entity = output.Entity;

            entity.Film.Name.Should().Be(film.Name);
        }

        [Fact]
        public async Task GetFilmWithFilmPersonReturnsNotFound()
        {
            _filmHandler.Setup(method => method.GetFilmWithFilmPerson(It.IsAny<int>())).ReturnsAsync((FilmEntity)null);

            var output = await _filmPagesManager.GetFilmWithFilmPerson(2);

            _filmHandler.Verify(method => method.GetFilmWithFilmPerson(It.IsAny<int>()), Times.Once);
            _mapper.Verify(method => method.Map<Film>(It.IsAny<FilmEntity>()), Times.Never);

            output.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task SaveFilmCallsMethodsAsRequiredAndReturnsBool(bool isDuplicate)
        {
            var film = new Film { Id = 1, Name = "Test" };
            var filmEntity = new FilmEntity { Id = 1, Name = "Test" };

            _filmHandler.Setup(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(isDuplicate);
            _mapper.Setup(method => method.Map<FilmEntity>(It.IsAny<Film>())).Returns(filmEntity);
            _filmHandler.Setup(method => method.SaveFilm(It.IsAny<FilmEntity>()));

            var output = await _filmPagesManager.SaveFilm(film);

            _filmHandler.Verify(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>()), Times.Once);

            if (isDuplicate)
            {
                _mapper.Verify(method => method.Map<FilmEntity>(It.IsAny<Film>()), Times.Never);
                _filmHandler.Verify(method => method.SaveFilm(It.IsAny<FilmEntity>()), Times.Never);
            }
            else
            {
                _mapper.Verify(method => method.Map<FilmEntity>(It.IsAny<Film>()), Times.Once);
                _filmHandler.Verify(method => method.SaveFilm(It.IsAny<FilmEntity>()), Times.Once);
            }

            output.Should().Be(!isDuplicate);
        }

        [Fact]
        public async Task RemoveActorsFromFilmCallsRemoveFilmPersonForEachFilmPerson()
        {
            var filmPerson1 = new FilmPerson();
            var filmPerson2 = new FilmPerson();

            var filmPersonList = new List<FilmPerson> { filmPerson1, filmPerson2 };

            var filmPersonEntity1 = new FilmPersonEntity();
            var filmPersonEntity2 = new FilmPersonEntity();

            _mapper.SetupSequence(method => method.Map<FilmPersonEntity>(It.IsAny<FilmPerson>())).Returns(filmPersonEntity1).Returns(filmPersonEntity2);

            await _filmPagesManager.RemoveActorsFromFilm(filmPersonList);

            _mapper.Verify(method => method.Map<FilmPersonEntity>(It.IsAny<FilmPerson>()), Times.Exactly(filmPersonList.Count));
            _filmPersonHandler.Verify(method => method.RemoveFilmPerson(It.IsAny<FilmPersonEntity>()),
                Times.Exactly(filmPersonList.Count));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UpdateFilmCallsMethodsAsRequiredAndReturnsBool(bool isDuplicate)
        {
            var film = new Film { Id = 1, Name = "Test" };
            var filmEntity = new FilmEntity { Id = 1, Name = "Test" };

            _filmHandler.Setup(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(isDuplicate);
            _mapper.Setup(method => method.Map<FilmEntity>(It.IsAny<Film>())).Returns(filmEntity);
            _filmHandler.Setup(method => method.UpdateFilm(It.IsAny<FilmEntity>()));

            var output = await _filmPagesManager.UpdateFilm(film);

            _filmHandler.Verify(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>()), Times.Once);

            if (isDuplicate)
            {
                _mapper.Verify(method => method.Map<FilmEntity>(It.IsAny<Film>()), Times.Never);
                _filmHandler.Verify(method => method.UpdateFilm(It.IsAny<FilmEntity>()), Times.Never);
            }
            else
            {
                _mapper.Verify(method => method.Map<FilmEntity>(It.IsAny<Film>()), Times.Once);
                _filmHandler.Verify(method => method.UpdateFilm(It.IsAny<FilmEntity>()), Times.Once);
            }

            output.Should().Be(!isDuplicate);
        }

        [Fact]
        public async Task GetFilmsAndGenresReturnsFilmPagesValues()
        {
            var filmEntity1 = new FilmEntity { Name = "Film1" };
            var filmEntity2 = new FilmEntity { Name = "Film2" };
            var filmEntities = new List<FilmEntity> { filmEntity1, filmEntity2 };

            var film1 = new Film { Name = "Film1" };
            var film2 = new Film { Name = "Film2" };
            var films = new List<Film> { film1, film2 };

            var genreEntity = new GenreEntity { Name = "A New Genre" };
            var genreEntities = new List<GenreEntity> { genreEntity };

            var genre = new Genre { Name = "A New Genre" };
            var genres = new List<Genre> { genre };

            _filmHandler.Setup(method => method.GetFilms()).ReturnsAsync(filmEntities);
            _mapper.Setup(method => method.Map<List<Film>>(It.IsAny<List<FilmEntity>>())).Returns(films);

            _genreHandler.Setup(method => method.GetGenres()).ReturnsAsync(genreEntities);
            _mapper.Setup(method => method.Map<List<Genre>>(It.IsAny<List<GenreEntity>>())).Returns(genres);

            var output = await _filmPagesManager.GetFilmsAndGenres();

            _filmHandler.Verify(method => method.GetFilms(),Times.Once);
            _mapper.Verify(method => method.Map<List<Film>>(It.IsAny<List<FilmEntity>>()), Times.Once);

            _genreHandler.Verify(method => method.GetGenres(), Times.Once);
            _mapper.Verify(method => method.Map<List<Genre>>(It.IsAny<List<GenreEntity>>()),Times.Once);

            output.Films.Count.Should().Be(films.Count);
            output.Genres.Count.Should().Be(genres.Count + 1);

            output.Films.Should().Contain(film1);
            output.Films.Should().Contain(film2);
            output.Genres.Should().Contain(genre);
            output.Genres.Should().ContainEquivalentOf(new Genre
            {
                Id = PageValues.Zero,
                Name = PageValues.All
            });
        }
    }
}