using FilmReference.DataAccess;
using System.Threading.Tasks;
using FilmReference.DataAccess.DbClasses;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IFilmPersonHandler
    {
        Task RemoveFilmPerson(FilmPersonEntity filmPerson);
    }
}