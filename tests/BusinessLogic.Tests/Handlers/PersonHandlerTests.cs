using BusinessLogic.Handlers;
using FilmReference.DataAccess.Entities;
using FilmReference.DataAccess.Repositories;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MockQueryable.Moq;
using Xunit;

namespace BusinessLogic.Tests.Handlers
{
    public class PersonHandlerTests
    {
        private readonly Mock<IGenericRepository<PersonEntity>> _personRepository;
        private readonly PersonHandler _personHandler;

        public PersonHandlerTests()
        {
            _personRepository = new Mock<IGenericRepository<PersonEntity>>();
            _personHandler = new PersonHandler(_personRepository.Object);
        }

        [Fact]
        public async void GetDirectorsCallsRepository()
        {
            var personEntity1 = new PersonEntity {PersonId = 1, FullName = "Test1", IsDirector = true};
            var personEntity2 = new PersonEntity { PersonId = 1, FullName = "ATest", IsDirector = true };

            var personList = new List<PersonEntity>{personEntity1, personEntity2};

            _personRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()))
                .ReturnsAsync(personList);

            var output = await _personHandler.GetDirectors();

            _personRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()), Times.Once);

            var personEntities = output.ToList();
            personEntities.Count().Should().Be(personList.Count);

            var entity = personEntities.ElementAt(0);

            entity.Should().BeEquivalentTo(personEntity2);

            personEntities.Should().ContainEquivalentOf(personEntity1);
        }

        [Fact]
        public async void GetActorsCallsRepository()
        {
            var personEntity1 = new PersonEntity { PersonId = 1, FullName = "Test1", IsActor = true };
            var personEntity2 = new PersonEntity { PersonId = 1, FullName = "ATest", IsActor = true };

            var personList = new List<PersonEntity> { personEntity1, personEntity2 };

            _personRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()))
                .ReturnsAsync(personList);

            var output = await _personHandler.GetActors();


            _personRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()), Times.Once);
            
            var personEntities = output.ToList();
            personEntities.Count().Should().Be(personList.Count);

            var entity = personEntities.ElementAt(0);

            entity.Should().BeEquivalentTo(personEntity2);

            personEntities.Should().ContainEquivalentOf(personEntity1);
        }

        [Fact]
        public async void SavePersonCallsRepository()
        {
            var personEntity1 = new PersonEntity { PersonId = 1, FullName = "Test1", IsActor = true };

            await _personHandler.SavePerson(personEntity1);

            _personRepository.Verify(method => method.Save(), Times.Once);
        }

        [Fact]
        public async void IsDuplicateReturnsFalse()
        {
            const int id = 1;
            var personEntity = new PersonEntity { PersonId = id, FirstName = "Test", LastName = "Person" };

            _personRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()))
                .ReturnsAsync(new List<PersonEntity>());

            var output = await _personHandler.IsDuplicate(personEntity);

            _personRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()), Times.Once);

            output.Should().BeFalse();
        }

        [Fact]
        public async void IsDuplicateSameRecordReturnsFalse()
        {
            const int id = 1;
            var personEntity = new PersonEntity { PersonId = id, FirstName = "Test", LastName = "Person" };

            _personRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()))
                .ReturnsAsync(new List<PersonEntity>{ new PersonEntity{PersonId = id}});

            var output = await _personHandler.IsDuplicate(personEntity);

            _personRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()), Times.Once);

            output.Should().BeFalse();
        }

        [Fact]
        public async void IsDuplicateNewRecordReturnsTrue()
        {
            const int id = 1;
            var personEntity = new PersonEntity { PersonId = 0, FirstName = "Test", LastName = "Person" };

            _personRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()))
                .ReturnsAsync(new List<PersonEntity> { new PersonEntity { PersonId = id } });

            var output = await _personHandler.IsDuplicate(personEntity);

            _personRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()), Times.Once);

            output.Should().BeTrue();
        }

        [Fact]
        public async void IsDuplicateExistingRecordReturnsTrue()
        {
            const int id = 1;
            var personEntity = new PersonEntity { PersonId = 2, FirstName = "Test", LastName = "Person" };

            _personRepository.Setup(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()))
                .ReturnsAsync(new List<PersonEntity> { new PersonEntity { PersonId = id } });

            var output = await _personHandler.IsDuplicate(personEntity);

            _personRepository.Verify(method => method.GetWhere(It.IsAny<Expression<Func<PersonEntity, bool>>>()), Times.Once);

            output.Should().BeTrue();
        }

        [Fact]
        public async void GetPersonWithDetails()
        {
            const int id = 4;

            var person = new PersonEntity {PersonId = 1, FirstName = "Test"};
            var film = new FilmEntity {FilmId = 2, Name = "film1"};
            var filmPerson = new FilmPersonEntity {FilmPersonId = 3, Person = person, Film = film, FilmId = film.FilmId, PersonId = person.PersonId};

            person.FilmPerson = new List<FilmPersonEntity>{filmPerson};

            var person2 = new PersonEntity { PersonId = 4, FirstName = "Test2" };
            var film2 = new FilmEntity { FilmId = 5, Name = "film2" };
            var filmPerson2 = new FilmPersonEntity { FilmPersonId = 6, Person = person2, Film = film2, FilmId = film2.FilmId, PersonId = person2.PersonId };

            person2.FilmPerson = new List<FilmPersonEntity> { filmPerson2 };

           var personList = new List<PersonEntity>{person, person2};

           var personQueryable = personList.AsQueryable().BuildMock();

           _personRepository.Setup(method => method.GetAllQueryable()).Returns(personQueryable.Object);

           var output = await _personHandler.GetPersonWithDetails(id);

           _personRepository.Verify(method => method.GetAllQueryable(), Times.Once);

            output.Should().BeEquivalentTo(person2);
        }

        [Fact]
        public async void GetPersonByIdCallsRepository()
        {
            const int id = 4;

            var person = new PersonEntity { PersonId = 1, FirstName = "Test" };

            _personRepository.Setup(method => method.GetById(It.IsAny<int>())).ReturnsAsync(person);

            var output = await _personHandler.GetPersonById(id);

            _personRepository.Verify(method => method.GetById(It.IsAny<int>()),Times.Once);

            output.Should().BeEquivalentTo(person);
        }

        [Fact]
        public async void UpdatePersonCallsRepository()
        {
            var person = new PersonEntity { PersonId = 1, FirstName = "Test" };

            await _personHandler.UpdatePerson(person);

            _personRepository.Verify(method => method.Update(It.IsAny<PersonEntity>()), Times.Once);
        }

        [Fact]
        public async void GetActorsByCharacterCallsRepository()
        {
            const string startCharacter = "A";
            var person = new PersonEntity { PersonId = 1, FirstName = "Test", IsActor = true , FullName = "Test Test"};
            var film = new FilmEntity { FilmId = 2, Name = "film1" };
            var filmPerson = new FilmPersonEntity { FilmPersonId = 3, Person = person, Film = film, FilmId = film.FilmId, PersonId = person.PersonId };

            person.FilmPerson = new List<FilmPersonEntity> { filmPerson };

            var person2 = new PersonEntity { PersonId = 4, FirstName = $"{startCharacter}xx" , IsActor = true, FullName = $"{startCharacter}xx test"};
            var film2 = new FilmEntity { FilmId = 5, Name = "film2" };
            var filmPerson2 = new FilmPersonEntity { FilmPersonId = 6, Person = person2, Film = film2, FilmId = film2.FilmId, PersonId = person2.PersonId };

            person2.FilmPerson = new List<FilmPersonEntity> { filmPerson2 };

            var person3= new PersonEntity { PersonId = 7, FirstName = $"{startCharacter}Test2", IsActor = false, FullName = "A Nother"};
            var filmPerson3 = new FilmPersonEntity { FilmPersonId = 8, Person = person3, Film = film2, FilmId = film2.FilmId, PersonId = person3.PersonId };

            person3.FilmPerson = new List<FilmPersonEntity> { filmPerson3 };

            var person4 = new PersonEntity { PersonId = 9, FirstName = $"{startCharacter}Test2", IsActor = true, FullName = $"{startCharacter}n Actor"};
            var filmPerson4 = new FilmPersonEntity { FilmPersonId = 10, Person = person4, Film = film2, FilmId = film2.FilmId, PersonId = person4.PersonId };

            person4.FilmPerson = new List<FilmPersonEntity> { filmPerson4};

            var personList = new List<PersonEntity> { person, person2 , person3, person4};

            var personQueryable = personList.AsQueryable().BuildMock();

            _personRepository.Setup(method => method.GetAllQueryable()).Returns(personQueryable.Object);

            var output = await _personHandler.GetActors($"{startCharacter}");

            _personRepository.Verify(method => method.GetAllQueryable(), Times.Once);

            var personEntities = output.ToList();

            personEntities.Count.Should().Be(2);

            var entity = personEntities.ElementAt(0);

            entity.Should().BeEquivalentTo(person4);

            personEntities.Should().ContainEquivalentOf(person2);
        }
    }
}
