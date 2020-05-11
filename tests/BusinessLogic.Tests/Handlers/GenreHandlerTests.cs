using BusinessLogic.Handlers;
using FilmReference.DataAccess.Entities;
using FilmReference.DataAccess.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace BusinessLogic.Tests.Handlers
{
    public class GenreHandlerTests
    {
        private readonly Mock<IGenericRepository<GenreEntity>> _genreRepository;
        private readonly GenreHandler _genreHandler;

        public GenreHandlerTests()
        {
            _genreRepository = new Mock<IGenericRepository<GenreEntity>>();
            _genreHandler = new GenreHandler(_genreRepository.Object);
        }

        [Fact]
        public async void IsDuplicateReturnsFalse()
        {
            var genreEntity = new GenreEntity();

            _genreRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<GenreEntity, bool>>>()))
                .ReturnsAsync(new List<GenreEntity>());

            var output = await _genreHandler.IsDuplicate(genreEntity);

            _genreRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<GenreEntity, bool>>>()), Times.Once);
               

            output.Should().BeFalse();
        }

        [Fact]
        public async void IsDuplicateSameRecordReturnsFalse()
        {
            const int id = 1;
            var genreEntity = new GenreEntity { Id = id};

            _genreRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<GenreEntity, bool>>>()))
                .ReturnsAsync(new List<GenreEntity>{ new GenreEntity{ Id = id}});

            var output = await _genreHandler.IsDuplicate(genreEntity);

            _genreRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<GenreEntity, bool>>>()), Times.Once);

            output.Should().BeFalse();
        }

        [Fact]
        public async void IsDuplicateNewRecordReturnsTrue()
        {
            const int id = 1;
            var genreEntity = new GenreEntity { Id = 0 };

            _genreRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<GenreEntity, bool>>>()))
                .ReturnsAsync(new List<GenreEntity> { new GenreEntity { Id = id } });

            var output = await _genreHandler.IsDuplicate(genreEntity);

            _genreRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<GenreEntity, bool>>>()), Times.Once);

            output.Should().BeTrue();
        }

        [Fact]
        public async void IsDuplicateExistingRecordReturnsTrue()
        {
            const int id = 1;
            var genreEntity = new GenreEntity { Id = 2 };

            _genreRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<GenreEntity, bool>>>()))
                .ReturnsAsync(new List<GenreEntity> { new GenreEntity { Id = id } });

            var output = await _genreHandler.IsDuplicate(genreEntity);

            _genreRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<GenreEntity, bool>>>()), Times.Once);

            output.Should().BeTrue();
        }

        [Fact]
        public async void GetGenreByIdCallsRepository()
        {
            const int id = 1;
            var genreEntity = new GenreEntity { Id = 10, Name = "Test"};

            _genreRepository.Setup(method => method.GetById(It.IsAny<int>())).ReturnsAsync(genreEntity);

            var output = await _genreHandler.GetGenreById(id);

            _genreRepository.Verify(method => method.GetById(It.IsAny<int>()), Times.Once);

            output.Should().BeEquivalentTo(genreEntity);
        }

        [Fact]
        public async void UpdateGenreCallsRepository()
        {
            var genreEntity = new GenreEntity { Id = 10, Name = "Test" };

            await _genreHandler.UpdateGenre(genreEntity);

            _genreRepository.Verify(method => method.Update(It.IsAny<GenreEntity>()), Times.Once);
        }

        [Fact]
        public async void SaveGenreCallsRepository()
        {
            var genreEntity = new GenreEntity { Id = 10, Name = "Test" };

            await _genreHandler.SaveGenre(genreEntity);

            _genreRepository.Verify(method => method.Add(It.IsAny<GenreEntity>()), Times.Once);
            _genreRepository.Verify(method => method.Save(), Times.Once);
        }
    }
}