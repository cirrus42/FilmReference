
using System.Threading.Tasks;
using FilmReference.DataAccess;

namespace FilmReference.FrontEnd.Managers
{
    public interface IPersonPagesManager
    {
        Task<bool> SavePerson(Person person);
    }
}