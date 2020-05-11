using System.Collections.Generic;
using System.Linq;
using FilmReference.DataAccess;
using FilmReference.DataAccess.Entities;
using FilmReference.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessTests
{
    public class RepositoryTests
    {
        private readonly FilmReferenceContext _filmReferenceContext;
        private readonly IGenericRepository<PersonEntity> _genericRepository;
        private readonly IGenericRepository<FilmEntity> _genericFilmRepository;

        public RepositoryTests()
        {
            var options = new DbContextOptionsBuilder<FilmReferenceContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _filmReferenceContext = new FilmReferenceContext(options);

            _genericRepository = new GenericRepository<PersonEntity>(_filmReferenceContext);
            _genericFilmRepository = new GenericRepository<FilmEntity>(_filmReferenceContext);
        }

        [Fact]
        public async void GetAllReturnsAllInstancesOfEntity()
        {
            var person1 = new PersonEntity()
            {
                Id = 1,
                FirstName = "Person1",
                LastName = "Lastname1",
                Description = "Person 1",
                IsActor = true,
                IsDirector = false,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            var person2 = new PersonEntity()
            {
                Id = 2,
                FirstName = "Person2",
                LastName = "Lastname2",
                Description = "Person 2",
                IsActor = true,
                IsDirector = false,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            await _filmReferenceContext.Person.AddAsync(person1);
            await _filmReferenceContext.Person.AddAsync(person2);
            await _filmReferenceContext.SaveChangesAsync();

            var personList = (await _genericRepository.GetAll()).ToList();

            personList.Count.Should().Be(2);
            personList.Should().Contain(person1);
            personList.Should().Contain(person2);
        }


        [Fact]
        public async void GetWhereReturnsAllInstancesOfEntity()
        {
            var person1 = new PersonEntity()
            {
                Id = 1,
                FirstName = "Person1",
                LastName = "Lastname1",
                Description = "Person 1",
                IsActor = true,
                IsDirector = false,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            var person2 = new PersonEntity()
            {
                Id = 2,
                FirstName = "Person2",
                LastName = "Lastname2",
                Description = "Person 2",
                IsActor = false,
                IsDirector = true,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            await _filmReferenceContext.Person.AddAsync(person1);
            await _filmReferenceContext.Person.AddAsync(person2);
            await _filmReferenceContext.SaveChangesAsync();

            var personList = (await _genericRepository.GetWhere(x => x.IsActor)).ToList();

            personList.Count.Should().Be(1);
            personList.Should().Contain(person1);
        }

        [Fact]
        public async void GetAllQueryableReturnsAllInstancesOfEntity()
        {
            var person1 = new PersonEntity()
            {
                Id = 1,
                FirstName = "Person1",
                LastName = "Lastname1",
                Description = "Person 1",
                IsActor = true,
                IsDirector = false,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            var person2 = new PersonEntity()
            {
                Id = 2,
                FirstName = "Person2",
                LastName = "Lastname2",
                Description = "Person 2",
                IsActor = false,
                IsDirector = true,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            await _filmReferenceContext.Person.AddAsync(person1);
            await _filmReferenceContext.Person.AddAsync(person2);
            await _filmReferenceContext.SaveChangesAsync();

            var personList =  _genericRepository.GetAllQueryable().Where(x => x.IsDirector).ToList();

            personList.Count.Should().Be(1);
            personList.Should().Contain(person2);
        }

        [Fact]
        public async void GetByIdReturnsEntity()
        {
            var person1 = new PersonEntity()
            {
                Id = 1,
                FirstName = "Person1",
                LastName = "Lastname1",
                Description = "Person 1",
                IsActor = true,
                IsDirector = false,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            var person2 = new PersonEntity()
            {
                Id = 2,
                FirstName = "Person2",
                LastName = "Lastname2",
                Description = "Person 2",
                IsActor = false,
                IsDirector = true,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            await _filmReferenceContext.Person.AddAsync(person1);
            await _filmReferenceContext.Person.AddAsync(person2);
            await _filmReferenceContext.SaveChangesAsync();

            var person = await _genericRepository.GetById(person2.Id);

            person.Should().Be(person2);
        }

        [Fact]
        public void Update()
        {
            var film = new FilmEntity{ Id = 1, Name = "Film1" };

            _filmReferenceContext.Film.Add(film);
            _filmReferenceContext.SaveChanges();
            const string name = "New Name";
            film.Name = name;

            _genericFilmRepository.Add(film);
            _genericFilmRepository.Save();

            var dbFilm = _filmReferenceContext.Find<FilmEntity>(1);
            dbFilm.Name.Should().Be(name);
        }

        [Fact]
        public void AddAndSaveAddsThenSaves()
        {
            var person1 = new PersonEntity()
            {
                Id = 1,
                FirstName = "Person1",
                LastName = "Lastname1",
                Description = "Person 1",
                IsActor = true,
                IsDirector = false,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            var person2 = new PersonEntity()
            {
                Id = 2,
                FirstName = "Person2",
                LastName = "Lastname2",
                Description = "Person 2",
                IsActor = true,
                IsDirector = false,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            _filmReferenceContext.Person.Add(person1);
            _filmReferenceContext.Person.Add(person2);
            _filmReferenceContext.SaveChanges();

            var person3 = new PersonEntity()
            {
                Id = 3,
                FirstName = "Person3",
                LastName = "Lastname3",
                Description = "Person 3",
                IsActor = true,
                IsDirector = false,
                Film = new List<FilmEntity>(),
                FilmPerson = new List<FilmPersonEntity> { new FilmPersonEntity() }
            };

            _genericRepository.Add(person3);
            _genericRepository.Save();

            _filmReferenceContext.Person.Count().Should().Be(3);

            var dbPerson = _filmReferenceContext.Find<PersonEntity>(3);

            dbPerson.FirstName.Should().Be(person3.FirstName);
            dbPerson.LastName.Should().Be(person3.LastName);
        }

        [Fact]
        public void DeleteRemovesRecord()
        {
            var film1 = new FilmEntity() { Id = 1, Name = "Film1" };
            var film2 = new FilmEntity() { Id = 2, Name = "Film2" };

            _filmReferenceContext.Film.Add(film1);
            _filmReferenceContext.Film.Add(film2);
            _filmReferenceContext.SaveChanges();

            _genericFilmRepository.Delete(film1);
            _genericFilmRepository.Save();

            _filmReferenceContext.Film.Count().Should().Be(1);

            var dbFilm = _filmReferenceContext.Set<FilmEntity>().ToList().First();

            dbFilm.Name.Should().Be(film2.Name);
        }
    }
}
