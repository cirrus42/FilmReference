using System.Threading.Tasks;
using Shared.Models;
using PersonEntity = FilmReference.DataAccess.DbClasses.PersonEntity;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IPersonPagesManager
    {
        Task<bool> SavePerson(PersonEntity person);
        Task<Results<PersonPagesValues>> GetPersonDetails(int id);
        Task<Results<PersonEntity>> GetPersonById(int id);
        Task<bool> UpdatePerson(PersonEntity person);
    }
}