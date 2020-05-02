using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Handlers.Interfaces;
using FilmReference.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using PersonEntity = FilmReference.DataAccess.Entities.PersonEntity;

namespace BusinessLogic.Handlers
{
    public class PersonHandler : IPersonHandler
    {
        private readonly IGenericRepository<PersonEntity> _personRepository;

        public PersonHandler(IGenericRepository<PersonEntity> personRepository) =>
            _personRepository = personRepository;

        public async Task<IEnumerable<PersonEntity>> GetDirectors() =>
            (await _personRepository.GetWhere(person => person.IsDirector)).OrderBy(person => person.FullName);
        public async Task<IEnumerable<PersonEntity>> GetActors() =>
            (await _personRepository.GetWhere(person => person.IsActor)).OrderBy(person => person.FullName);

        public async Task SavePerson(PersonEntity person)
        {
            await _personRepository.Add(person);
            await _personRepository.Save();
        }
        
        public async Task<bool> IsDuplicate(PersonEntity person)
        {
            var duplicates =
                (await _personRepository.GetWhere(p =>
                    p.FirstName.ToLower().Replace(" ", "") == person.FirstName.ToLower().Replace(" ", "") &&
                    p.LastName.ToLower().Replace(" ", "") == person.LastName.ToLower().Replace(" ","")
                )).ToList();

            if (!duplicates.Any()) return false;

            return person.PersonId <= 0 || duplicates.Any(p => p.PersonId != person.PersonId);
        }

        public async Task<PersonEntity> GetPersonWithDetails(int id) => 
            await _personRepository.GetAllQueryable()
                .Include(p => p.FilmPerson)
                .ThenInclude(fp => fp.Film)
                .FirstOrDefaultAsync(m => m.PersonId == id);

        public async Task<PersonEntity> GetPersonById(int id) =>
            await _personRepository.GetById(id);

        public Task UpdatePerson(PersonEntity person) =>
            _personRepository.Update(person);

        public async Task<IEnumerable<PersonEntity>> GetActors(string startCharacter) =>
            await _personRepository.GetAllQueryable()
                .Include(p => p.FilmPerson)
                .Where(p => p.IsActor && p.FullName.StartsWith(startCharacter))
                .OrderBy(p => p.FullName)
                .ToListAsync();
    }
}