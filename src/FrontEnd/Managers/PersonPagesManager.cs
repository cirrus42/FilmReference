using AutoMapper;
using FilmReference.DataAccess.Entities;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Managers.Interfaces;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Managers
{
    public class PersonPagesManager : IPersonPagesManager
    {
        private readonly IPersonHandler _personHandler;
        private readonly IMapper _mapper;

        public PersonPagesManager(IPersonHandler personHandler, IMapper mapper)
        {
            _personHandler = personHandler;
            _mapper = mapper;
        }

        public async Task<bool> SavePerson(Person person)
        {
            if (await _personHandler.IsDuplicate(_mapper.Map<PersonEntity>(person)))
                return false;

            await _personHandler.SavePerson(_mapper.Map<PersonEntity>(person));
            return true;
        }

        public async Task<Results<PersonPagesValues>> GetPersonDetails(int id)
        {
            var personEntity = await _personHandler.GetPersonWithDetails(id);

            if (personEntity == null) return new Results<PersonPagesValues> {HttpStatusCode = HttpStatusCode.NotFound};

            var filmPersonList = _mapper.Map<List<FilmPerson>>(personEntity.FilmPerson.OrderBy(fp => fp.Film.Name));

            return new Results<PersonPagesValues>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Entity = new PersonPagesValues
                {
                    Person = _mapper.Map<Person>(personEntity),
                    Films = filmPersonList.Select(filmPerson => filmPerson.Film).ToList()
                }
            };
        }

        public async Task<Results<Person>> GetPersonById(int id)
        {
            var person = _mapper.Map<Person>(await _personHandler.GetPersonWithDetails(id));
            return person == null ? 
                new Results<Person> {HttpStatusCode = HttpStatusCode.NotFound} : 
                new Results<Person> {HttpStatusCode = HttpStatusCode.OK, Entity = person};
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            if (await _personHandler.IsDuplicate(_mapper.Map<PersonEntity>(person)))
                return false;
            await _personHandler.UpdatePerson(_mapper.Map<PersonEntity>(person));
            return true;
        }
    }
}