using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IPersonHandler
    {
        Task<IEnumerable<Person>> GetDirectors();
        Task<IEnumerable<Person>> GetActors();
    }
}