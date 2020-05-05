using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using BusinessLogic.Handlers;
using FilmReference.DataAccess.Entities;
using FilmReference.DataAccess.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace BusinessLogic.Tests.Handlers
{
    public class StudioHandlerTests
    {
        private readonly Mock<IGenericRepository<StudioEntity>> _studioRepository;
        private readonly StudioHandler _studioHandler;

        public StudioHandlerTests()
        {
            _studioRepository = new Mock<IGenericRepository<StudioEntity>>();
            _studioHandler = new StudioHandler(_studioRepository.Object);
        }

        [Fact]
        public async void GetStudiosCallsRepository()
        {
            var studio = new StudioEntity {StudioId = 1, Name = "Test",};
            var studio2 = new StudioEntity { StudioId = 2, Name = "Ax Test" };
            var studio3 = new StudioEntity { StudioId = 3, Name = "AnotherTest" };

            var studioList = new List<StudioEntity>{studio, studio2, studio3};

            _studioRepository.Setup(method => method.GetAll()).ReturnsAsync(studioList);

            var output = await _studioHandler.GetStudios();

            var studios = output.ToList();

            var entity = studios.ElementAt(0);

            entity.Should().BeEquivalentTo(studio3);

            studios.Should().ContainEquivalentOf(studio);
            studios.Should().ContainEquivalentOf(studio2);
        }

        [Fact]
        public async void IsDuplicateReturnsFalse()
        {
            const int id = 1;
            var studioEntity = new StudioEntity {StudioId = id};

            _studioRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<StudioEntity, bool>>>()))
                .ReturnsAsync(new List<StudioEntity>());

            var output = await _studioHandler.IsDuplicate(studioEntity);

            _studioRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<StudioEntity, bool>>>()), Times.Once);
            
            output.Should().BeFalse();
        }

        [Fact]
        public async void IsDuplicateSameRecordReturnsFalse()
        {
            const int id = 1;
            var studioEntity = new StudioEntity { StudioId = id };

            _studioRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<StudioEntity, bool>>>()))
                .ReturnsAsync(new List<StudioEntity>{new StudioEntity{StudioId = id}});

            var output = await _studioHandler.IsDuplicate(studioEntity);

            _studioRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<StudioEntity, bool>>>()), Times.Once);

            output.Should().BeFalse();
        }

        [Fact]
        public async void IsDuplicateNewRecordReturnsTrue()
        {
            const int id = 1;
            var studioEntity = new StudioEntity { StudioId = 0 };

            _studioRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<StudioEntity, bool>>>()))
                .ReturnsAsync(new List<StudioEntity> { new StudioEntity { StudioId = id } });

            var output = await _studioHandler.IsDuplicate(studioEntity);

            _studioRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<StudioEntity, bool>>>()), Times.Once);

            output.Should().BeTrue();
        }

        [Fact]
        public async void IsDuplicateExistingRecordReturnsTrue()
        {
            const int id = 1;
            var studioEntity = new StudioEntity { StudioId = 2 };

            _studioRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<StudioEntity, bool>>>()))
                .ReturnsAsync(new List<StudioEntity> { new StudioEntity { StudioId = id } });

            var output = await _studioHandler.IsDuplicate(studioEntity);

            _studioRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<StudioEntity, bool>>>()), Times.Once);

            output.Should().BeTrue();
        }

        [Fact]
        public async void SaveStudioCallsRepository()
        {
            var studioEntity = new StudioEntity { StudioId = 2 };

            await _studioHandler.SaveStudio(studioEntity);

            _studioRepository.Verify(method => method.Add(It.IsAny<StudioEntity>()), Times.Once);
            _studioRepository.Verify(method => method.Save(), Times.Once);
        }

        [Fact]
        public async void UpdateStudioCallsRepository()
        {
            var studioEntity = new StudioEntity { StudioId = 2 };

            await _studioHandler.UpdateStudio(studioEntity);

            _studioRepository.Verify(method => method.Update(It.IsAny<StudioEntity>()), Times.Once);
        }

        [Fact]
        public async void GetStudioByIdCallsRepository()
        {
            const int id = 1;
            var studioEntity = new StudioEntity { StudioId = 2 };

            _studioRepository.Setup(method => method.GetById(It.IsAny<int>())).ReturnsAsync(studioEntity);

            var output = await _studioHandler.GetStudioById(id);

            _studioRepository.Verify(method => method.GetById(It.IsAny<int>()), Times.Once);

            output.Should().BeEquivalentTo(studioEntity);
        }


    }
}
