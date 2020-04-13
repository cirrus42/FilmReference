using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Handlers;

namespace FilmReference.FrontEnd.Managers
{
    public class FilmPagesManager : IFilmPagesManager
    {
        private readonly IPersonHandler _personHandler;

        public FilmPagesManager(IPersonHandler personHandler)
        {
            _personHandler = personHandler;
        }
    }
}
