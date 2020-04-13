using System.Collections.Generic;
using System.Threading.Tasks;
using FilmReference.DataAccess;

namespace FilmReference.FrontEnd.Handlers
{
    public interface IPersonHandler
    {
        Task<IEnumerable<Person>> GetDirectors();
        Task<IEnumerable<Person>> GetActors();
    }
}