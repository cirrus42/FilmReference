using FilmReference.DataAccess.Entities;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    internal interface IFilmPersonHandler
    {
        Task RemoveFilmPerson(FilmPersonEntity filmPerson);
    }
}