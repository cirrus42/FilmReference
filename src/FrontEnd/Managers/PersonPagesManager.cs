using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Managers.Interfaces;
using FilmReference.FrontEnd.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Managers
{
    public class PersonPagesManager : IPersonPagesManager
    {
        private readonly IPersonHandler _personHandler;

        public PersonPagesManager(IPersonHandler personHandler) =>
            _personHandler = personHandler;
        
        public async Task<bool> SavePerson(Person person)
        {
            if (await _personHandler.IsDuplicate(person))
                return false;

            await _personHandler.SavePerson(person);
            return true;
        }

        public async Task<Results<PersonPagesValues>> GetPersonDetails(int id)
        {
            var person = await _personHandler.GetPerson(id);

            if (person == null) return new Results<PersonPagesValues> {HttpStatusCode = HttpStatusCode.NotFound};

            var filmPersonList = person.FilmPerson.OrderBy(fp => fp.Film.Name);

            return new Results<PersonPagesValues>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Entity = new PersonPagesValues
                {
                    Person = person, 
                    Films = filmPersonList.Select(filmPerson => filmPerson.Film).ToList()
                }
            };

        }
    }
}