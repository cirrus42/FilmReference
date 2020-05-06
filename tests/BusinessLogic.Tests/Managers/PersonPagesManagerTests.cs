using AutoMapper;
using BusinessLogic.Handlers.Interfaces;
using BusinessLogic.Managers;
using BusinessLogic.Models;
using BusinessLogic.Validations;
using FilmReference.DataAccess.Entities;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BusinessLogic.Tests.Managers
{
    public class PersonPagesManagerTests
    {
        private readonly Mock<IPersonHandler> _personHandler;
        private readonly Mock<IPersonValidator> _personValidator;
        private readonly Mock<IMapper> _mapper;
        private readonly PersonPagesManager _personPagesManager;

        public PersonPagesManagerTests()
        {
            _personHandler = new Mock<IPersonHandler>();
            _personValidator = new Mock<IPersonValidator>();
            _mapper = new Mock<IMapper>();
            _personPagesManager = new PersonPagesManager(_personHandler.Object, _mapper.Object, _personValidator.Object);
        }

        [Fact]
        public async void SavePersonValidationPassesAndReturnsEmptyList()
        {
            var person = new Person();
            var personEntity = new PersonEntity();

            _personValidator.Setup(method => method.ValidatePerson(It.IsAny<Person>())).Returns(new List<string>());
            _mapper.Setup(method => method.Map<PersonEntity>(It.IsAny<Person>())).Returns(personEntity);
            _personHandler.Setup(method => method.IsDuplicate(It.IsAny<PersonEntity>())).ReturnsAsync(false);
         
            var output = await _personPagesManager.SavePerson(person);

            _personValidator.Verify(method => method.ValidatePerson(It.IsAny<Person>()), Times.Once());
            _mapper.Verify(method => method.Map<PersonEntity>(It.IsAny<Person>()), Times.Once);
            _personHandler.Verify(method => method.IsDuplicate(It.IsAny<PersonEntity>()), Times.Once);
            _personHandler.Verify(method => method.SavePerson(It.IsAny<PersonEntity>()), Times.Once);

            var outputValidationList = output.ToList();

            outputValidationList.Should().BeEmpty();
        }
    }
}
