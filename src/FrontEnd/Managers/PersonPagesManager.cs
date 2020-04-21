using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
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
    }
}
