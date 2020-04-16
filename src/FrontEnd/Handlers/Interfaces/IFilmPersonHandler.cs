using FilmReference.DataAccess;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IFilmPersonHandler
    {
        Task RemoveFilmPerson(FilmPerson filmPerson);
    }
}