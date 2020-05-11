using System.Net;
using AutoMapper;
using BusinessLogic.Handlers.Interfaces;
using BusinessLogic.Managers;
using BusinessLogic.Models;
using FilmReference.DataAccess.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace BusinessLogic.Tests.Managers
{
    public class StudioPagesManagerTests
    {
        private readonly Mock<IStudioHandler> _studioHandler;
        private readonly Mock<IMapper> _mapper;
        private readonly StudioPagesManager _studioPagesManager;

        public StudioPagesManagerTests()
        {
            _studioHandler = new Mock<IStudioHandler>();
            _mapper = new Mock<IMapper>();
            _studioPagesManager = new StudioPagesManager(_studioHandler.Object, _mapper.Object);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void SaveStudioReturnsCorrectBoolean(bool isDuplicate)
        {
            var studio = new Studio();
            var studioEntity = new StudioEntity();

            _mapper.Setup(method => method.Map<StudioEntity>(It.IsAny<Studio>())).Returns(studioEntity);
            _studioHandler.Setup(method => method.IsDuplicate(It.IsAny<StudioEntity>())).ReturnsAsync(isDuplicate);

            var output = await _studioPagesManager.SaveStudio(studio);

            _mapper.Verify(method => method.Map<StudioEntity>(It.IsAny<Studio>()), Times.Once);
            _studioHandler.Verify(method => method.IsDuplicate(It.IsAny<StudioEntity>()),Times.Once);

            if (isDuplicate)
                _studioHandler.Verify(method => method.SaveStudio(studioEntity), Times.Never);
            else
                _studioHandler.Verify(method => method.SaveStudio(studioEntity), Times.Once);

            output.Should().Be(!isDuplicate);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void UpdateStudioReturnsCorrectBoolean(bool isDuplicate)
        {
            var studio = new Studio();
            var studioEntity = new StudioEntity();

            _mapper.Setup(method => method.Map<StudioEntity>(It.IsAny<Studio>())).Returns(studioEntity);
            _studioHandler.Setup(method => method.IsDuplicate(It.IsAny<StudioEntity>())).ReturnsAsync(isDuplicate);

            var output = await _studioPagesManager.UpdateStudio(studio);

            _mapper.Verify(method => method.Map<StudioEntity>(It.IsAny<Studio>()), Times.Once);
            _studioHandler.Verify(method => method.IsDuplicate(It.IsAny<StudioEntity>()), Times.Once);

            if (isDuplicate)
                _studioHandler.Verify(method => method.UpdateStudio(studioEntity), Times.Never);
            else
                _studioHandler.Verify(method => method.UpdateStudio(studioEntity), Times.Once);

            output.Should().Be(!isDuplicate);
        }

        [Fact]
        public async void GetStudioByIdReturnsOk()
        {
            const int id = 1;
            var studio = new Studio();
            var studioEntity = new StudioEntity();

            _studioHandler.Setup(method => method.GetStudioById(It.IsAny<int>())).ReturnsAsync(studioEntity);
            _mapper.Setup(method => method.Map<Studio>(It.IsAny<StudioEntity>())).Returns(studio);

            var output = await _studioPagesManager.GetStudioById(id);

            _studioHandler.Verify(method => method.GetStudioById(It.IsAny<int>()), Times.Once);
            _mapper.Verify(method => method.Map<Studio>(It.IsAny<StudioEntity>()), Times.Once);

            output.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void GetStudioByIdReturnsNotFound()
        {
            const int id = 1;

            _studioHandler.Setup(method => method.GetStudioById(It.IsAny<int>())).ReturnsAsync((StudioEntity)null);
            _mapper.Setup(method => method.Map<Studio>(It.IsAny<StudioEntity>())).Returns((Studio)null);

            var output = await _studioPagesManager.GetStudioById(id);

            _studioHandler.Verify(method => method.GetStudioById(It.IsAny<int>()), Times.Once);
            _mapper.Verify(method => method.Map<Studio>(It.IsAny<StudioEntity>()), Times.Once);

            output.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
