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
using System.Net;
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
            _personPagesManager =
                new PersonPagesManager(_personHandler.Object, _mapper.Object, _personValidator.Object);
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

        [Fact]
        public async void SavePersonValidationFailsPersonValidationAndReturnsList()
        {
            var person = new Person();
            var personEntity = new PersonEntity();

            _personValidator.Setup(method => method.ValidatePerson(It.IsAny<Person>()))
                .Returns(new List<string> {PageValues.PersonNameValidation});
            _mapper.Setup(method => method.Map<PersonEntity>(It.IsAny<Person>())).Returns(personEntity);
            var output = await _personPagesManager.SavePerson(person);

            _personValidator.Verify(method => method.ValidatePerson(It.IsAny<Person>()), Times.Once());
            _mapper.Verify(method => method.Map<PersonEntity>(It.IsAny<Person>()), Times.Once);
            _personHandler.Verify(method => method.IsDuplicate(It.IsAny<PersonEntity>()), Times.Never);
            _personHandler.Verify(method => method.SavePerson(It.IsAny<PersonEntity>()), Times.Never);

            var outputValidationList = output.ToList();

            outputValidationList.Should().HaveCount(1);
        }

        [Fact]
        public async void SavePersonValidationFailsDuplicateValidationAndReturnsList()
        {
            var person = new Person();
            var personEntity = new PersonEntity();

            _personValidator.Setup(method => method.ValidatePerson(It.IsAny<Person>()))
                .Returns(new List<string>());
            _mapper.Setup(method => method.Map<PersonEntity>(It.IsAny<Person>())).Returns(personEntity);
            _personHandler.Setup(method => method.IsDuplicate(It.IsAny<PersonEntity>())).ReturnsAsync(true);

            var output = await _personPagesManager.SavePerson(person);

            _personValidator.Verify(method => method.ValidatePerson(It.IsAny<Person>()), Times.Once());
            _mapper.Verify(method => method.Map<PersonEntity>(It.IsAny<Person>()), Times.Once);
            _personHandler.Verify(method => method.IsDuplicate(It.IsAny<PersonEntity>()), Times.Once);
            _personHandler.Verify(method => method.SavePerson(It.IsAny<PersonEntity>()), Times.Never);

            var outputValidationList = output.ToList();

            outputValidationList.Should().HaveCount(1);
        }

        [Fact]
        public async void UpdatePersonValidationPersonValidationFailsAndReturnsPopulatedList()
        {
            var person = new Person();
            var personEntity = new PersonEntity();

            _personValidator.Setup(method => method.ValidatePerson(It.IsAny<Person>()))
                .Returns(new List<string> {PageValues.PersonNameValidation});
            _mapper.Setup(method => method.Map<PersonEntity>(It.IsAny<Person>())).Returns(personEntity);
            _personHandler.Setup(method => method.IsDuplicate(It.IsAny<PersonEntity>())).ReturnsAsync(false);

            var output = await _personPagesManager.UpdatePerson(person);

            _personValidator.Verify(method => method.ValidatePerson(It.IsAny<Person>()), Times.Once());
            _mapper.Verify(method => method.Map<PersonEntity>(It.IsAny<Person>()), Times.Once);
            _personHandler.Verify(method => method.IsDuplicate(It.IsAny<PersonEntity>()), Times.Once);
            _personHandler.Verify(method => method.UpdatePerson(It.IsAny<PersonEntity>()), Times.Never);

            var outputValidationList = output.ToList();
            outputValidationList.Should().HaveCount(1);
        }

        [Fact]
        public async void UpdatePersonValidationDuplicateValidationFailsAndReturnsPopulatedList()
        {
            var person = new Person();
            var personEntity = new PersonEntity();

            _personValidator.Setup(method => method.ValidatePerson(It.IsAny<Person>()))
                .Returns(new List<string>());
            _mapper.Setup(method => method.Map<PersonEntity>(It.IsAny<Person>())).Returns(personEntity);
            _personHandler.Setup(method => method.IsDuplicate(It.IsAny<PersonEntity>())).ReturnsAsync(true);

            var output = await _personPagesManager.UpdatePerson(person);

            _personValidator.Verify(method => method.ValidatePerson(It.IsAny<Person>()), Times.Once());
            _mapper.Verify(method => method.Map<PersonEntity>(It.IsAny<Person>()), Times.Once);
            _personHandler.Verify(method => method.IsDuplicate(It.IsAny<PersonEntity>()), Times.Once);
            _personHandler.Verify(method => method.UpdatePerson(It.IsAny<PersonEntity>()), Times.Never);

            var outputValidationList = output.ToList();
            outputValidationList.Should().HaveCount(1);
            outputValidationList.Should().Contain(PageValues.PersonDuplicateValidation);
        }

        [Fact]
        public async void GetPersonDetailsReturnsPersonPagesValues()
        {
            const int id = 1;
            var filmEntity = new FilmEntity() {FilmId = 1, Name = "Test"};
            var filmPersonEntity = new FilmPersonEntity {Film = filmEntity};
            var filmPersonEntityList = new List<FilmPersonEntity> {filmPersonEntity};
            var personEntity = new PersonEntity {FilmPerson = filmPersonEntityList};

            var film = new Film {Id = 1};
            var filmPerson = new FilmPerson {Film = film};
            var filmPersonList = new List<FilmPerson> {filmPerson};
            var person = new Person {FilmPerson = filmPersonList};

            _personHandler.Setup(method => method.GetPersonWithDetails(It.IsAny<int>())).ReturnsAsync(personEntity);
            _mapper.Setup(method => method.Map<List<FilmPerson>>(It.IsAny<List<FilmPersonEntity>>()))
                .Returns(filmPersonList);
            _mapper.Setup(method => method.Map<Person>(It.IsAny<PersonEntity>())).Returns(person);

            var output = await _personPagesManager.GetPersonDetails(id);

            _personHandler.Verify(method => method.GetPersonWithDetails(It.IsAny<int>()), Times.Once);
            _mapper.Verify(method => method.Map<List<FilmPerson>>(It.IsAny<List<FilmPersonEntity>>()), Times.Once);
            _mapper.Verify(method => method.Map<Person>(It.IsAny<PersonEntity>()), Times.Once);

            output.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void GetPersonDetailsReturnsNotFound()
        {
            const int id = 1;

            _personHandler.Setup(method => method.GetPersonWithDetails(It.IsAny<int>()))
                .ReturnsAsync((PersonEntity) null);

            var output = await _personPagesManager.GetPersonDetails(id);

            _personHandler.Verify(method => method.GetPersonWithDetails(It.IsAny<int>()), Times.Once);
            _mapper.Verify(method => method.Map<List<FilmPerson>>(It.IsAny<List<FilmPersonEntity>>()), Times.Never);
            _mapper.Verify(method => method.Map<Person>(It.IsAny<PersonEntity>()), Times.Never);

            output.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void GetPersonByIdReturnsPerson()
        {
            const int id = 1;
            var person = new Person();
            var personEntity = new PersonEntity();

            _personHandler.Setup(method => method.GetPersonWithDetails(It.IsAny<int>())).ReturnsAsync(personEntity);
            _mapper.Setup(method => method.Map<Person>(It.IsAny<PersonEntity>())).Returns(person);

            var output = await _personPagesManager.GetPersonById(id);

            output.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void GetPersonByIdReturnsNotFound()
        {
            const int id = 1;
    
            _personHandler.Setup(method => method.GetPersonWithDetails(It.IsAny<int>()))
                .ReturnsAsync((PersonEntity)null);
            _mapper.Setup(method => method.Map<Person>(It.IsAny<PersonEntity>())).Returns((Person)null);

            var output = await _personPagesManager.GetPersonById(id);

            output.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void GetDirectorsCallsRepository()
        {
            var personEntity = new PersonEntity();
            var personEntityList = new List<PersonEntity>{personEntity};

            _personHandler.Setup(method => method.GetDirectors()).ReturnsAsync(personEntityList);
            _mapper.Setup(method => method.Map<IEnumerable<Person>>(It.IsAny<IEnumerable<PersonEntity>>()));

            var output = await _personPagesManager.GetDirectors();

            _personHandler.Verify(method => method.GetDirectors(), Times.Once);
            _mapper.Verify(method => method.Map<IEnumerable<Person>>(It.IsAny<IEnumerable<PersonEntity>>()), Times.Once);
        }

        [Fact]
        public async void GetActorssCallsRepository()
        {
            var personEntity = new PersonEntity();
            var personEntityList = new List<PersonEntity> { personEntity };
            const string id = "s";
            _personHandler.Setup(method => method.GetActors(It.IsAny<string>())).ReturnsAsync(personEntityList);
            _mapper.Setup(method => method.Map<IEnumerable<Person>>(It.IsAny<IEnumerable<PersonEntity>>()));

            var output = await _personPagesManager.GetActors(id);

            _personHandler.Verify(method => method.GetActors(It.IsAny<string>()), Times.Once);
            _mapper.Verify(method => method.Map<IEnumerable<Person>>(It.IsAny<IEnumerable<PersonEntity>>()), Times.Once);
        }
    }
}