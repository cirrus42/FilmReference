using System.Threading.Tasks;
using FilmReference.DataAccess.Entities;

namespace BusinessLogic.Handlers.Interfaces
{
    public interface IFilmPersonHandler
    {
        Task RemoveFilmPerson(FilmPersonEntity filmPerson);
    }
}