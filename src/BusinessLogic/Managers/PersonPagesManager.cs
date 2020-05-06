using AutoMapper;
using BusinessLogic.Handlers.Interfaces;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Validations;
using FilmReference.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    public class PersonPagesManager : IPersonPagesManager
    {
        private readonly IPersonHandler _personHandler;
        private readonly IPersonValidator _personValidator;
        private readonly IMapper _mapper;

        public PersonPagesManager(IPersonHandler personHandler, IMapper mapper, IPersonValidator personValidator)
        {
            _personHandler = personHandler;
            _mapper = mapper;
            _personValidator = personValidator;
        }

        public async Task<IEnumerable<string>> SavePerson(Person person)
        {
            var validationList =  _personValidator.ValidatePerson(person).ToList();
            var personEntity = _mapper.Map<PersonEntity>(person);

            if (!validationList.Contains(PageValues.PersonNameValidation))
            {
                if (await _personHandler.IsDuplicate(personEntity))
                    validationList.Add(PageValues.PersonDuplicateValidation);
            }

            if (validationList.Count == 0)
                await _personHandler.SavePerson(personEntity);

            return validationList;
        }

        public async Task<IEnumerable<string>> UpdatePerson(Person person)
        {
            var validationList =  _personValidator.ValidatePerson(person).ToList();
            var personEntity = _mapper.Map<PersonEntity>(person);

            if (await _personHandler.IsDuplicate(personEntity))
                validationList.Add(PageValues.PersonDuplicateValidation);

            if (validationList.Count == 0)
                await _personHandler.UpdatePerson(personEntity);

            return validationList;
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

        public async Task<IEnumerable<Person>> GetDirectors() => 
            _mapper.Map<IEnumerable<Person>>(await _personHandler.GetDirectors());

        public async Task<IEnumerable<Person>> GetActors(string id) => 
            _mapper.Map<IEnumerable<Person>>(await _personHandler.GetActors(id));
    }
}