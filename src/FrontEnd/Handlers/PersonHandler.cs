using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers
{
    public class PersonHandler : IPersonHandler
    {
        private readonly IGenericRepository<Person> _personRepository;

        public PersonHandler(IGenericRepository<Person> personRepository) =>
            _personRepository = personRepository;

        public async Task<IEnumerable<Person>> GetDirectors() =>
            (await _personRepository.GetWhere(person => person.IsDirector)).OrderBy(person => person.FullName);
        public async Task<IEnumerable<Person>> GetActors() =>
            (await _personRepository.GetWhere(person => person.IsActor)).OrderBy(person => person.FullName);
    }
}