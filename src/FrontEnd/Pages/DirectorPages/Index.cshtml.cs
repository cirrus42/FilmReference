using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.DirectorPages
{
    public class IndexModel : PageModel
    {
        private readonly IPersonHandler _personHandler;
        public IList<Person> PersonList { get; set; }

        public IndexModel( IPersonHandler personHandler) =>
            _personHandler = personHandler;
        
        public async Task OnGetAsync() =>
            PersonList = (await _personHandler.GetDirectors()).ToList();

    }
}