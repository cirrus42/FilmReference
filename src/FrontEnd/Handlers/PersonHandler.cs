using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using FilmReference.FrontEnd.Handlers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public Task SavePerson(Person person) =>
            _personRepository.Add(person);

        public async Task<bool> IsDuplicate(Person person)
        {
            var duplicates =
                (await _personRepository.GetWhere(p =>
                    p.FirstName.ToLower().Replace(" ", "") == person.FirstName.ToLower().Replace(" ", "") &&
                    p.LastName.ToLower().Replace(" ", "") == person.LastName.ToLower().Replace(" ","")
                )).ToList();

            if (!duplicates.Any()) return false;

            return person.PersonId <= 0 || duplicates.Any(p => p.PersonId != person.PersonId);
        }

        public async Task<Person> GetPerson(int id) => 
            await _personRepository.GetAllQueryable()
                .Include(p => p.FilmPerson)
                .ThenInclude(fp => fp.Film)
                .FirstOrDefaultAsync(m => m.PersonId == id);

    }
}