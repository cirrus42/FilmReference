﻿using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Managers;
using FilmReference.FrontEnd.Models;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FilmReference.Tests
{
    public class FilmPagesManagerTests
    {
        private readonly Mock<IPersonHandler> _personHandler;
        private readonly Mock<IGenreHandler> _genreHandler;
        private readonly Mock<IStudioHandler> _studioHandler;
        private readonly Mock<IFilmHandler> _filmHandler;
        private readonly Mock<IFilmPersonHandler> _filmPersonHandler;
        private FilmPagesManager _filmPagesManager;

        public FilmPagesManagerTests()
        {
            _personHandler = new Mock<IPersonHandler>();
            _genreHandler = new Mock<IGenreHandler>();
            _studioHandler = new Mock<IStudioHandler>();
            _filmHandler = new Mock<IFilmHandler>();
            _filmPersonHandler = new Mock<IFilmPersonHandler>();

            _filmPagesManager = new FilmPagesManager(_personHandler.Object,_genreHandler.Object,_studioHandler.Object,_filmHandler.Object,_filmPersonHandler.Object);
        }

        [Fact]
        public async Task GetFilmByIdReturnsFilm()
        {
            var film = new Film {Name = "Film"};
            var person = new Person {FirstName = "Test", LastName = "Surname"};
            var actors = new List<Person> {person};
            var filmDetails = new FilmDetails {Film = film, Actors = actors};

            var result = new Results<FilmDetails> {HttpStatusCode = HttpStatusCode.OK, Entity = filmDetails};

            _filmHandler.Setup(method => method.GetFilmById(It.IsAny<int>())).ReturnsAsync(result);

            var methodResult = await _filmPagesManager.GetFilmById(1);

            _filmHandler.Verify(method => method.GetFilmById(It.IsAny<int>()), Times.Once);

            methodResult.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            var entity = methodResult.Entity;

            entity.Film.Name.Should().Be(film.Name);
            entity.Actors.Count.Should().Be(1);
            entity.Actors.Should().Contain(person);
        }

        [Fact]
        public async Task GetFilmPageDropDownValuesReturnsFilmPagesValues()
        {
            var director = new Person {FirstName = "Steven", LastName = "Speelberger"};
            var directors = new List<Person>{director};

            var actor = new Person { FirstName = "Ed", LastName = "Wood" };
            var actors = new List<Person> { actor };

            var genre = new Genre {Name = "A New Genre"};
            var genres = new List<Genre>{genre};

            var studio = new Studio {Name = "Studio"};
            var studios = new List<Studio>{studio};

            _personHandler.Setup(method => method.GetDirectors()).ReturnsAsync(directors);
            _personHandler.Setup(method => method.GetActors()).ReturnsAsync(actors);
            _genreHandler.Setup(method => method.GetGenres()).ReturnsAsync(genres);
            _studioHandler.Setup(method => method.GetStudios()).ReturnsAsync(studios);

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

            output.Directors.Should().ContainEquivalentOf(new Person
            {
                PersonId = PageValues.MinusOne,
                FullName = PageValues.PleaseSelect
            });

            output.Genres.Should().ContainEquivalentOf(new Genre
            {
                GenreId = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            });

            output.Studios.Should().ContainEquivalentOf(new Studio
            {
                StudioId = PageValues.MinusOne,
                Name = PageValues.PleaseSelect
            });
        }

        [Fact]
        public async Task GetFilmWithFilmPersonReturnsFilmValues()
        {
            var film = new Film { Name = "Film" };
            var person = new Person { FirstName = "Test", LastName = "Surname" };
            var actors = new List<Person> { person };
            var filmDetails = new FilmDetails { Film = film, Actors = actors };

            var result = new Results<FilmDetails> { HttpStatusCode = HttpStatusCode.OK, Entity = filmDetails };

            _filmHandler.Setup(method => method.GetFilmWithFilmPerson(It.IsAny<int>())).ReturnsAsync(result);

            var output = await _filmPagesManager.GetFilmWithFilmPerson(2);

            _filmHandler.Verify(method => method.GetFilmWithFilmPerson(It.IsAny<int>()), Times.Once);

            output.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            var entity = output.Entity;

            entity.Film.Name.Should().Be(film.Name);
            entity.Actors.Count.Should().Be(1);
            entity.Actors.Should().Contain(person);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task SaveFilmCallsMethodsAsRequiredAndReturnsBool(bool isDuplicate)
        {
            var film = new Film{FilmId = 1, Name = "Test"};
            _filmHandler.Setup(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(isDuplicate);

            _filmHandler.Setup(method => method.SaveFilm(It.IsAny<Film>()));

            var output = await _filmPagesManager.SaveFilm(film);

            _filmHandler.Verify(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>()), Times.Once);

            if (isDuplicate)
                _filmHandler.Verify(method => method.SaveFilm(It.IsAny<Film>()),Times.Never);
            else
                _filmHandler.Verify(method => method.SaveFilm(It.IsAny<Film>()), Times.Once);

            output.Should().Be(!isDuplicate);
        }

        [Fact]
        public async Task RemoveActorsFromFilmCallsRemoveFilmPersonForEachFilmPerson()
        {
            var filmPerson1 = new FilmPerson();
            var filmPerson2 = new FilmPerson();

            var filmPersonList = new List<FilmPerson>{filmPerson1, filmPerson2};

            await _filmPagesManager.RemoveActorsFromFilm(filmPersonList);

            _filmPersonHandler.Verify(method => method.RemoveFilmPerson(It.IsAny<FilmPerson>()),
                Times.Exactly(filmPersonList.Count));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UpdateFilmCallsMethodsAsRequiredAndReturnsBool(bool isDuplicate)
        {
            var film = new Film { FilmId = 1, Name = "Test" };
            _filmHandler.Setup(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(isDuplicate);

            _filmHandler.Setup(method => method.UpdateFilm(It.IsAny<Film>()));

            var output = await _filmPagesManager.UpdateFilm(film);

            _filmHandler.Verify(method => method.IsDuplicate(It.IsAny<int>(), It.IsAny<string>()), Times.Once);

            if (isDuplicate)
                _filmHandler.Verify(method => method.UpdateFilm(It.IsAny<Film>()), Times.Never);
            else
                _filmHandler.Verify(method => method.UpdateFilm(It.IsAny<Film>()), Times.Once);

            output.Should().Be(!isDuplicate);
        }

        [Fact]
        public async Task GetFilmsAndGenresReturnsFilmPagesValues()
        {
            var film1 = new Film { Name = "Film1" };
            var film2 = new Film { Name = "Film2" };
            var films = new List<Film>{film1, film2};

            var genre = new Genre { Name = "A New Genre" };
            var genres = new List<Genre> { genre };

            _filmHandler.Setup(method => method.GetFilms()).ReturnsAsync(films);
            _genreHandler.Setup(method => method.GetGenres()).ReturnsAsync(genres);

            var output = await _filmPagesManager.GetFilmsAndGenres();

            output.Films.Count.Should().Be(films.Count);
            output.Genres.Count.Should().Be(genres.Count + 1);

            output.Films.Should().Contain(film1);
            output.Films.Should().Contain(film2);
            output.Genres.Should().Contain(genre);
            output.Genres.Should().ContainEquivalentOf(new Genre
            {
                GenreId = PageValues.Zero,
                Name = PageValues.All
            });
        }
    }
}
