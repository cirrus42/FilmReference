using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Managers.Interfaces
{
    public interface IPersonPagesManager
    {
        Task<IEnumerable<string>> SavePerson(Person person);
        Task<IEnumerable<string>> UpdatePerson(Person person);
        Task<Results<PersonPagesValues>> GetPersonDetails(int id);
        Task<Results<Person>> GetPersonById(int id);
        Task<IEnumerable<Person>> GetDirectors();
        Task <IEnumerable<Person>>GetActors(string id);
    }
}