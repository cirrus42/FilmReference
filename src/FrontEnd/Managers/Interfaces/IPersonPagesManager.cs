using System.Threading.Tasks;
using FilmReference.DataAccess;
using FilmReference.FrontEnd.Models;

namespace FilmReference.FrontEnd.Managers.Interfaces
{
    public interface IPersonPagesManager
    {
        Task<bool> SavePerson(Person person);
        Task<Results<PersonPagesValues>> GetPersonDetails(int id);
    }
}