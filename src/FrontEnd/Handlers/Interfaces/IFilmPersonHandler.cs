using FilmReference.DataAccess.Entities;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IFilmPersonHandler
    {
        Task RemoveFilmPerson(FilmPersonEntity filmPerson);
    }
}