using System.Collections.Generic;
using System.Threading.Tasks;
using PersonEntity = FilmReference.DataAccess.Entities.PersonEntity;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    internal interface IPersonHandler
    {
        Task<IEnumerable<PersonEntity>> GetDirectors();
        Task<IEnumerable<PersonEntity>> GetActors();
        Task SavePerson(PersonEntity person);
        Task<bool> IsDuplicate(PersonEntity person);
        Task<PersonEntity> GetPersonWithDetails(int id);
        Task<PersonEntity> GetPersonById(int id);
        Task UpdatePerson(PersonEntity person);
        Task<IEnumerable<PersonEntity>> GetActors(string startCharacter);
    }
}