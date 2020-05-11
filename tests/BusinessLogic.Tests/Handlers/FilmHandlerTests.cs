using BusinessLogic.Handlers;
using FilmReference.DataAccess.Entities;
using FilmReference.DataAccess.Repositories;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogic.Tests.Handlers
{
    public class FilmHandlerTests
    {
        private readonly Mock<IGenericRepository<FilmEntity>> _filmRepository;
        private readonly FilmHandler _filmHandler;

        public FilmHandlerTests()
        {
            _filmRepository = new Mock<IGenericRepository<FilmEntity>>();
            _filmHandler = new FilmHandler(_filmRepository.Object);
        }

        [Fact]
        public async Task SaveFilmCallsRepositoryMethods()
        {
            var filmEntity = new FilmEntity();
            await _filmHandler.SaveFilm(filmEntity);

            _filmRepository.Verify(method => method.Add(It.IsAny<FilmEntity>()), Times.Once);
            _filmRepository.Verify(method => method.Save(), Times.Once);
        }

        [Fact]
        public async Task UpdateFilmCallsRepositoryMethods()
        {
            var filmEntity = new FilmEntity();
            await _filmHandler.UpdateFilm(filmEntity);

            _filmRepository.Verify(method => method.Update(It.IsAny<FilmEntity>()), Times.Once);
        }

        [Fact]
        public async Task GetFilmByIdCallsRepositoryMethod()
        {
            var director = new PersonEntity { Id = 1, IsDirector = true, FirstName = "Test", LastName = "Test" };
            var genre = new GenreEntity { Id = 1, Name = "Genre" };
            var studio = new StudioEntity { Id = 1, Name = "Studio" };
            var person = new PersonEntity { Id = 2, IsActor = true, FirstName = "Actor", LastName = "Lastname" };
            var filmPerson = new FilmPersonEntity { Person = person, Id = 1, PersonId = person.Id };
            var film = new FilmEntity
            {
                Id = 1,
                Genre = genre,
                Studio = studio,
                Director = director,
                FilmPerson = new Collection<FilmPersonEntity> { filmPerson }
            };

            var director2 = new PersonEntity { Id = 3, IsDirector = true, FirstName = "Test", LastName = "Test" };
            var genre2 = new GenreEntity { Id = 2, Name = "Genre" };
            var studio2 = new StudioEntity { Id = 2, Name = "Studio" };
            var person2 = new PersonEntity { Id = 4, IsActor = true, FirstName = "Actor", LastName = "Lastname" };
            var filmPerson2 = new FilmPersonEntity { Person = person2, Id = 2, PersonId = person2.Id };
            var film2 = new FilmEntity
            {
                Id = 2,
                Genre = genre2,
                Studio = studio2,
                Director = director2,
                FilmPerson = new Collection<FilmPersonEntity> { filmPerson2 }
            };

            var filmsList = new List<FilmEntity> { film, film2 };

            var films = filmsList.AsQueryable().BuildMock();

            _filmRepository.Setup(method => method.GetAllQueryable()).Returns(films.Object);

            var output = await _filmHandler.GetFilmById(film.Id);

            _filmRepository.Verify(method => method.GetAllQueryable(), Times.Once);

            output.Id.Should().Be(film.Id);
            output.Director.Id.Should().Be(director.Id);
            output.Genre.Id.Should().Be(genre.Id);
            output.Studio.Id.Should().Be(studio.Id);
            output.FilmPerson.Count.Should().Be(1);

            var outputFilmPerson = output.FilmPerson.ElementAt(0);
            outputFilmPerson.Person.Id.Should().Be(person.Id);
        }

        [Fact]
        public async Task GetFilmWithFilmPersonCallsRepositoryMethod()
        {
            var director = new PersonEntity { Id = 1, IsDirector = true, FirstName = "Test", LastName = "Test" };
            var genre = new GenreEntity { Id = 1, Name = "Genre" };
            var studio = new StudioEntity { Id = 1, Name = "Studio" };
            var person = new PersonEntity { Id = 2, IsActor = true, FirstName = "Actor", LastName = "Lastname" };
            var filmPerson = new FilmPersonEntity { Person = person, Id = 1, PersonId = person.Id };
            var film = new FilmEntity
            {
                Id = 1,
                Genre = genre,
                Studio = studio,
                Director = director,
                FilmPerson = new Collection<FilmPersonEntity> { filmPerson }
            };

            var director2 = new PersonEntity { Id = 3, IsDirector = true, FirstName = "Test", LastName = "Test" };
            var genre2 = new GenreEntity { Id = 2, Name = "Genre" };
            var studio2 = new StudioEntity { Id = 2, Name = "Studio" };
            var person2 = new PersonEntity { Id = 4, IsActor = true, FirstName = "Actor", LastName = "Lastname" };
            var filmPerson2 = new FilmPersonEntity { Person = person2, Id = 2, PersonId = person2.Id };
            var film2 = new FilmEntity
            {
                Id = 2,
                Genre = genre2,
                Studio = studio2,
                Director = director2,
                FilmPerson = new Collection<FilmPersonEntity> { filmPerson2 }
            };

            var filmsList = new List<FilmEntity> { film, film2 };

            var films = filmsList.AsQueryable().BuildMock();

            _filmRepository.Setup(method => method.GetAllQueryable()).Returns(films.Object);

            var output = await _filmHandler.GetFilmWithFilmPerson(film.Id);

            _filmRepository.Verify(method => method.GetAllQueryable(), Times.Once);

            output.Id.Should().Be(film.Id);
            output.FilmPerson.Count.Should().Be(1);

            var outputFilmPerson = output.FilmPerson.ElementAt(0);
            outputFilmPerson.Person.Id.Should().Be(person.Id);
        }

        [Fact]
        public async Task GetFilmsCallsRepositoryMethod()
        {
            var film = new FilmEntity { Id = 1 };
            var film2 = new FilmEntity { Id = 2 };
            var film3 = new FilmEntity { Id = 3 };

            var filmList = new List<FilmEntity> { film, film2, film3 };

            _filmRepository.Setup(method => method.GetAll()).ReturnsAsync(filmList);

            var output = await _filmHandler.GetFilms();

            _filmRepository.Verify(method => method.GetAll(), Times.Once);

            var filmEntities = output.ToList();

            filmEntities.Count().Should().Be(filmList.Count);

            filmEntities.Should().Contain(film);
            filmEntities.Should().Contain(film2);
            filmEntities.Should().Contain(film3);
        }

        [Fact]
        public async Task IsDuplicateNoDuplicatesReturnsFalse()
        {
            const int filmId = 1;
            const string filmName = "film";

            _filmRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<FilmEntity, bool>>>()))
                .ReturnsAsync(new List<FilmEntity>());

            var output = await _filmHandler.IsDuplicate(filmId, filmName);

            _filmRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<FilmEntity, bool>>>()), Times.Once);

            output.Should().BeFalse();
        }

        [Fact]
        public async Task IsDuplicateSameRecordReturnsFalse()
        {
            const int filmId = 1;
            const string filmName = "film";

            _filmRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<FilmEntity, bool>>>()))
                .ReturnsAsync(new List<FilmEntity> { new FilmEntity { Id = 1, Name = "film" } });

            var output = await _filmHandler.IsDuplicate(filmId, filmName);

            _filmRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<FilmEntity, bool>>>()), Times.Once);

            output.Should().BeFalse();
        }

        [Fact]
        public async Task IsDuplicateNewRecordReturnsTrue()
        {
            const int filmId = 0;
            const string filmName = "film";

            _filmRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<FilmEntity, bool>>>()))
                .ReturnsAsync(new List<FilmEntity> { new FilmEntity { Id = 1, Name = "film" } });

            var output = await _filmHandler.IsDuplicate(filmId, filmName);

            _filmRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<FilmEntity, bool>>>()), Times.Once);

            output.Should().BeTrue();
        }


        [Fact]
        public async Task IsDuplicateExistingRecordReturnsTrue()
        {
            const int filmId = 1;
            const string filmName = "film";

            _filmRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<FilmEntity, bool>>>()))
                .ReturnsAsync(new List<FilmEntity> { new FilmEntity { Id = 2, Name = "film" } });

            var output = await _filmHandler.IsDuplicate(filmId, filmName);

            _filmRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<FilmEntity, bool>>>()), Times.Once);

            output.Should().BeTrue();
        }
    }
}