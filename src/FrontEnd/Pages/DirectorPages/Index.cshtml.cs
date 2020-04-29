using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonEntity = FilmReference.DataAccess.Entities.PersonEntity;

namespace FilmReference.FrontEnd.Pages.DirectorPages
{
    public class IndexModel : PageModel
    {
        private readonly IPersonHandler _personHandler;
        public IList<PersonEntity> PersonList { get; set; }

        public IndexModel( IPersonHandler personHandler) =>
            _personHandler = personHandler;
        
        public async Task OnGetAsync() =>
            PersonList = (await _personHandler.GetDirectors()).ToList();

    }
}