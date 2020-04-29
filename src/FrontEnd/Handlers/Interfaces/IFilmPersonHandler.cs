using FilmReference.DataAccess;
using System.Threading.Tasks;
using FilmReference.DataAccess.Entities;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IFilmPersonHandler
    {
        Task RemoveFilmPerson(FilmPersonEntity filmPerson);
    }
}