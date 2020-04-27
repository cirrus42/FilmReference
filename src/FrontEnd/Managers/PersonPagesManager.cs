using System.Collections.Generic;
using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Managers.Interfaces;
using FilmReference.FrontEnd.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PersonEntity = FilmReference.DataAccess.DbClasses.PersonEntity;

namespace FilmReference.FrontEnd.Managers
{
    public class PersonPagesManager : IPersonPagesManager
    {
        private readonly IPersonHandler _personHandler;

        public PersonPagesManager(IPersonHandler personHandler) =>
            _personHandler = personHandler;
        
        public async Task<bool> SavePerson(PersonEntity person)
        {
            if (await _personHandler.IsDuplicate(person))
                return false;

            await _personHandler.SavePerson(person);
            return true;
        }

        public async Task<Results<PersonPagesValues>> GetPersonDetails(int id)
        {
            throw new System.NotImplementedException();
            //var person = await _personHandler.GetPersonWithDetails(id);

            //if (person == null) return new Results<PersonPagesValues> {HttpStatusCode = HttpStatusCode.NotFound};

            //var filmPersonList = person.FilmPerson.OrderBy(fp => fp.Film.Name);

            //return new Results<PersonPagesValues>
            //{
            //    HttpStatusCode = HttpStatusCode.OK,
            //    Entity = new PersonPagesValues
            //    {
            //        Person = person, 
            //        Films = filmPersonList.Select(filmPerson => filmPerson.Film).ToList()
            //    }
            //};
        }

        public async Task<Results<PersonEntity>> GetPersonById(int id)
        {
            var person = await _personHandler.GetPersonWithDetails(id);
            return person == null ? 
                new Results<PersonEntity> {HttpStatusCode = HttpStatusCode.NotFound} : 
                new Results<PersonEntity> {HttpStatusCode = HttpStatusCode.OK, Entity = person};
        }

        public async Task<bool> UpdatePerson(PersonEntity person)
        {
            if (await _personHandler.IsDuplicate(person))
                return false;
            await _personHandler.UpdatePerson(person);
            return true;
        }

    }
}