using FilmReference.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IPersonHandler
    {
        Task<IEnumerable<Person>> GetDirectors();
        Task<IEnumerable<Person>> GetActors();
        Task SavePerson(Person person);
        Task<bool> IsDuplicate(Person person);
    }
}