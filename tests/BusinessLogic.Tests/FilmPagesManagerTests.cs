using AutoMapper;
using FilmReference.DataAccess.DbClasses;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Managers;
using FilmReference.FrontEnd.Models;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogic.Tests
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

            _filmPagesManager = new FilmPagesManager(_personHandler.Object,_genreHandler.Object,_studioHandler.Object,_filmHandler.Object,_filmPersonHandler.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetFilmByIdReturnsFilm()
        {
            var personEntity = new PersonEntity { FirstName = "Test", LastName = "Surname" };
            var filmPersonEntity = new FilmPersonEntity {Person = personEntity};
            var personEntityList = new Collection<FilmPersonEntity> {filmPersonEntity};

            var filmEntity = new FilmEntity { Name = "Film" , FilmPerson = personEntityList};
            var film = new Film {Name = "Film"};

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

            output.Directors.Count.Should().Be(2);
            output.Actors.Count.Should().Be(1);
            output.Genres.Count.Should().Be(2);
            output.Studios.Count.Should().Be(2);

            output.Directors.Should().Contain(directors);
            output.Actors.Should().Contain(actors);
            output.Genres.Should().Contain(genre);
            output.Studios.Should().Contain(studio);

            output.Directors.Should().ContainEquivalentOf(new PersonEntity
            {
                PersonId = PageValues.MinusOne,
                FullName = PageValues.PleaseSelect
            });

            output.Genres.Should().ContainEquivalentOf(new GenreEntity
            {
                GenreId = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            });

            output.Studios.Should().ContainEquivalentOf(new StudioEntity
            {
                StudioId = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            });
        }

        //[Fact]
        //public async Task GetFilmWithFilmPersonReturnsFilmValues()
        //{
        //    var film = new FilmEntity { Name = "Film" };
        //    var person = new PersonEntity { FirstName = "Test", LastName = "Surname" };
        //    var actors = new List<PersonEntity> { person };
        //    var filmDetails = new FilmDetails { Film = film, Actors = actors };

        //    var result = new Results<FilmDetails> { HttpStatusCode = HttpStatusCode.OK, Entity = filmDetails };

        //    _filmHandler.Setup(method => method.GetFilmWithFilmPerson(It.IsAny<int>())).ReturnsAsync(result);

        //    var output = await _filmPagesManager.GetFilmWithFilmPerson(2);

        //    _filmHandler.Verify(method => method.GetFilmWithFilmPerson(It.IsAny<int>()), Times.Once);

        //    output.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        //    var entity = output.Entity;

        //    entity.Film.Name.Should().Be(film.Name);
        //    entity.Actors.Count.Should().Be(1);
        //    entity.Actors.Should().Contain(person);
        //}

        //[Theory]
        //[InlineData(true)]
        //[InlineData(false)]
        //public async Task SaveFilmCallsMethodsAsRequiredAndReturnsBool(bool isDuplicate)
        //{
        //    var film = new FilmEntity{FilmId = 1, Name = "Test"};
        //    _filmHandler.Setup(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(isDuplicate);

        //    _filmHandler.Setup(method => method.SaveFilm(It.IsAny<FilmEntity>()));

        //    var output = await _filmPagesManager.SaveFilm(film);

        //    _filmHandler.Verify(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>()), Times.Once);

        //    if (isDuplicate)
        //        _filmHandler.Verify(method => method.SaveFilm(It.IsAny<FilmEntity>()),Times.Never);
        //    else
        //        _filmHandler.Verify(method => method.SaveFilm(It.IsAny<FilmEntity>()), Times.Once);

        //    output.Should().Be(!isDuplicate);
        //}

        //[Fact]
        //public async Task RemoveActorsFromFilmCallsRemoveFilmPersonForEachFilmPerson()
        //{
        //    var filmPerson1 = new FilmPersonEntity();
        //    var filmPerson2 = new FilmPersonEntity();

        //    var filmPersonList = new List<FilmPersonEntity>{filmPerson1, filmPerson2};

        //    await _filmPagesManager.RemoveActorsFromFilm(filmPersonList);

        //    _filmPersonHandler.Verify(method => method.RemoveFilmPerson(It.IsAny<FilmPersonEntity>()),
        //        Times.Exactly(filmPersonList.Count));
        //}

        //[Theory]
        //[InlineData(true)]
        //[InlineData(false)]
        //public async Task UpdateFilmCallsMethodsAsRequiredAndReturnsBool(bool isDuplicate)
        //{
        //    var film = new FilmEntity { FilmId = 1, Name = "Test" };
        //    _filmHandler.Setup(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(isDuplicate);

        //    _filmHandler.Setup(method => method.UpdateFilm(It.IsAny<FilmEntity>()));

        //    var output = await _filmPagesManager.UpdateFilm(film);

        //    _filmHandler.Verify(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>()), Times.Once);

        //    if (isDuplicate)
        //        _filmHandler.Verify(method => method.UpdateFilm(It.IsAny<FilmEntity>()), Times.Never);
        //    else
        //        _filmHandler.Verify(method => method.UpdateFilm(It.IsAny<FilmEntity>()), Times.Once);

        //    output.Should().Be(!isDuplicate);
        //}

        //[Fact]
        //public async Task GetFilmsAndGenresReturnsFilmPagesValues()
        //{
        //    var film1 = new FilmEntity { Name = "Film1" };
        //    var film2 = new FilmEntity { Name = "Film2" };
        //    var films = new List<FilmEntity>{film1, film2};

        //    var genre = new GenreEntity { Name = "A New Genre" };
        //    var genres = new List<GenreEntity> { genre };

        //    _filmHandler.Setup(method => method.GetFilms()).ReturnsAsync(films);
        //    _genreHandler.Setup(method => method.GetGenres()).ReturnsAsync(genres);

        //    var output = await _filmPagesManager.GetFilmsAndGenres();

        //    output.Films.Count.Should().Be(films.Count);
        //    output.Genres.Count.Should().Be(genres.Count + 1);

        //    output.Films.Should().Contain(film1);
        //    output.Films.Should().Contain(film2);
        //    output.Genres.Should().Contain(genre);
        //    output.Genres.Should().ContainEquivalentOf(new GenreEntity
        //    {
        //        GenreId = PageValues.Zero,
        //        Name = PageValues.All
        //    });
        //}
    }
}
