using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.Handlers.Interfaces;
using BusinessLogic.Managers;
using BusinessLogic.Models;
using FilmReference.DataAccess.Entities;
using FluentAssertions;
using Moq;
using System.Net;
using Xunit;

namespace BusinessLogic.Tests.Managers
{
    public class GenrePagesManagerTests
    {
        private readonly Mock<IGenreHandler> _genreHandler;
        private readonly Mock<IMapper> _mapper;
        private readonly GenrePagesManager _genrePagesManager;

        public GenrePagesManagerTests()
        {
            _genreHandler = new Mock<IGenreHandler>();
            _mapper = new Mock<IMapper>();
            _genrePagesManager = new GenrePagesManager(_genreHandler.Object, _mapper.Object);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void SaveGenreReturnsCorrectBoolean(bool isDuplicate)
        {
            var genre = new Genre();
            var genreEntity = new GenreEntity();

            _mapper.Setup(method => method.Map<GenreEntity>(It.IsAny<Genre>())).Returns(genreEntity);
            _genreHandler.Setup(method => method.IsDuplicate(It.IsAny<GenreEntity>())).ReturnsAsync(isDuplicate);

            var output = await _genrePagesManager.SaveGenre(genre);

            _mapper.Verify(method => method.Map<GenreEntity>(It.IsAny<Genre>()), Times.Once);
            _genreHandler.Verify(method => method.IsDuplicate(It.IsAny<GenreEntity>()));

            if(isDuplicate)
                _genreHandler.Verify(method => method.SaveGenre(genreEntity), Times.Never);
            else
                _genreHandler.Verify(method => method.SaveGenre(genreEntity), Times.Once);

            output.Should().Be(!isDuplicate);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void UpdateGenreReturnsCorrectBoolean(bool isDuplicate)
        {
            var genre = new Genre();
            var genreEntity = new GenreEntity();

            _genreHandler.Setup(method => method.IsDuplicate(It.IsAny<GenreEntity>())).ReturnsAsync(isDuplicate);
            _genreHandler.Setup(method => method.GetGenreById(It.IsAny<int>())).ReturnsAsync(genreEntity);

            var output = await _genrePagesManager.UpdateGenre(genre);

            _mapper.Verify(method => method.Map<GenreEntity>(It.IsAny<Genre>()),Times.Once);
            _genreHandler.Verify(method => method.IsDuplicate(It.IsAny<GenreEntity>()));

            if (isDuplicate)
            {
                _genreHandler.Verify(method => method.GetGenreById(It.IsAny<int>()), Times.Never);
                _genreHandler.Verify(method => method.UpdateGenre(genreEntity), Times.Never);
            }
            else
            {
                _genreHandler.Verify(method => method.GetGenreById(It.IsAny<int>()), Times.Once);
                _mapper.Verify(method => method.Map(It.IsAny<Genre>(), It.IsAny<GenreEntity>()), Times.Once());
                _genreHandler.Verify(method => method.UpdateGenre(It.IsAny<GenreEntity>()), Times.Once);
            }
            
            output.Should().Be(!isDuplicate);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void GetGenreByIdReturnsResults(bool isNull)
        {
            const int id = 1;
            var genreEntity = new GenreEntity();
            var genre = new Genre();

            _genreHandler.Setup(method => method.GetGenreById(It.IsAny<int>()))
                .ReturnsAsync(isNull ? null : genreEntity );

            _mapper.Setup(method => method.Map<Genre>(It.IsAny<GenreEntity>())).Returns(genre);

            _mapper.Setup(method => method.Map<GenreEntity>(It.IsAny<Genre>())).Returns(genreEntity);

            var result = await _genrePagesManager.GetGenreById(id);

            _genreHandler.Verify(method => method.GetGenreById(It.IsAny<int>()), Times.Once);

            if (isNull)
            {
                _mapper.Verify(method => method.Map<Genre>(It.IsAny<GenreEntity>()),Times.Never);
                result.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
            }
            else
            {
                _mapper.Verify(method => method.Map<Genre>(It.IsAny<GenreEntity>()), Times.Once);
                result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async void GetGenresCallsRepository()
        {
            var genreEntity = new GenreEntity();
            var genreEntityList = new List<GenreEntity> { genreEntity };
     
            _genreHandler.Setup(method => method.GetGenres()).ReturnsAsync(genreEntityList);
            _mapper.Setup(method => method.Map<IEnumerable<Genre>>(It.IsAny<IEnumerable<GenreEntity>>()));

            await _genrePagesManager.GetGenres();

            _genreHandler.Verify(method => method.GetGenres(), Times.Once);
            _mapper.Verify(method => method.Map<IEnumerable<Genre>>(It.IsAny<IEnumerable<GenreEntity>>()), Times.Once);
        }
    }
}