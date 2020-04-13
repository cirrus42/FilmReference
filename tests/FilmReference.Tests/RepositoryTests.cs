//using FilmReference.DataAccess;
//using FilmReference.DataAccess.Repositories;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using Xunit;

//namespace FilmReference.Tests
//{
//    public class RepositoryTests
//    {
//        private readonly FilmReferenceContext _filmReferenceContext;
//        private readonly IGenericRepository<Person> _genericRepository;
//        private readonly IGenericRepository<Film> _genericFilmRepository;

//        public RepositoryTests()
//        {
//            var options = new DbContextOptionsBuilder<FilmReferenceContext>()
//                .UseInMemoryDatabase(databaseName: "TestDb")
//                .Options;
//            _filmReferenceContext = new FilmReferenceContext(options);

//            _genericRepository = new GenericRepository<Person>(_filmReferenceContext);
//            _genericFilmRepository = new GenericRepository<Film>(_filmReferenceContext);
//        }

//        [Fact]
//        public void GetAllReturnsAllInstancesOfEntity()
//        {
//            var person1 = new Person(_filmReferenceContext)
//            {
//                PersonId = 1,
//                FirstName = "Person1",
//                LastName = "Lastname1",
//                Description = "Person 1",
//                IsActor = true,
//                IsDirector = false,
//                Film = new List<Film>(),
//                FilmPerson = new List<FilmPerson> {new FilmPerson()}
//            };

//            var person2 = new Person(_filmReferenceContext)
//            {
//                PersonId = 2,
//                FirstName = "Person2",
//                LastName = "Lastname2",
//                Description = "Person 2",
//                IsActor = true,
//                IsDirector = false,
//                Film = new List<Film>(),
//                FilmPerson = new List<FilmPerson> { new FilmPerson() }
//            };

//            _filmReferenceContext.Person.Add(person1);
//            _filmReferenceContext.Person.Add(person2);
//            _filmReferenceContext.SaveChanges();

//            var personList =  _genericRepository.GetAll().ToList();

//            personList.Count.Should().Be(2);
//            personList.Should().Contain(person1);
//            personList.Should().Contain(person2);
//        }

//        [Fact]
//        public void AddAndSaveAddsThenSaves()
//        {
//            var person1 = new Person(_filmReferenceContext)
//            {
//                PersonId = 1,
//                FirstName = "Person1",
//                LastName = "Lastname1",
//                Description = "Person 1",
//                IsActor = true,
//                IsDirector = false,
//                Film = new List<Film>(),
//                FilmPerson = new List<FilmPerson> { new FilmPerson() }
//            };

//            var person2 = new Person(_filmReferenceContext)
//            {
//                PersonId = 2,
//                FirstName = "Person2",
//                LastName = "Lastname2",
//                Description = "Person 2",
//                IsActor = true,
//                IsDirector = false,
//                Film = new List<Film>(),
//                FilmPerson = new List<FilmPerson> { new FilmPerson() }
//            };

//            _filmReferenceContext.Person.Add(person1);
//            _filmReferenceContext.Person.Add(person2);
//            _filmReferenceContext.SaveChanges();

//            var person3 = new Person(_filmReferenceContext)
//            {
//                PersonId = 3,
//                FirstName = "Person3",
//                LastName = "Lastname3",
//                Description = "Person 3",
//                IsActor = true,
//                IsDirector = false,
//                Film = new List<Film>(),
//                FilmPerson = new List<FilmPerson> { new FilmPerson() }
//            };

//            _genericRepository.Add(person3);
//            _genericRepository.Save();

//            _filmReferenceContext.Person.Count().Should().Be(3);

//           var dbPerson =  _filmReferenceContext.Find<Person>(3);

//           dbPerson.FirstName.Should().Be(person3.FirstName);
//           dbPerson.LastName.Should().Be(person3.LastName);
//        }

//        [Fact]
//        public void DeleteRemovesRecord()
//        {
//            var film1 = new Film(_filmReferenceContext) { FilmId = 1, Name = "Film1" };
//            var film2 = new Film(_filmReferenceContext) { FilmId = 2, Name = "Film2" };

//            _filmReferenceContext.Film.Add(film1);
//            _filmReferenceContext.Film.Add(film2);
//            _filmReferenceContext.SaveChanges();

//            _genericFilmRepository.Delete(film1);
//            _genericFilmRepository.Save();

//            _filmReferenceContext.Film.Count().Should().Be(1);

//            var dbFilm = _filmReferenceContext.Set<Film>().ToList().First();

//            dbFilm.Name.Should().Be(film2.Name);
//        }

//        //[Fact]
//        //public void Update()
//        //{
//        //    var film = new Film(_filmReferenceContext) { FilmId = 1, Name = "Film1" };

//        //    _filmReferenceContext.Film.Add(film);
//        //    _filmReferenceContext.SaveChanges();
//        //    const string name = "New Name";
//        //    film.Name = name;

//        //    _genericFilmRepository.Add(film);
//        //    _genericFilmRepository.Save();

//        //    var dbFilm = _filmReferenceContext.Find<Film>(1);
//        //    dbFilm.Name.Should().Be(name);
//        //}
//    }
//}
