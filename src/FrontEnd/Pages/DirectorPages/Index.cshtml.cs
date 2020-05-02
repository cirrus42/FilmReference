using FilmReference.FrontEnd.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.DirectorPages
{
    public class IndexModel : PageModel
    {
        private readonly IPersonPagesManager _personPagesManager;
        public IList<Person> PersonList { get; set; }

        public IndexModel( IPersonPagesManager personPagesManager) =>
            _personPagesManager = personPagesManager;
        
        public async Task OnGetAsync() =>
            PersonList = (await _personPagesManager.GetDirectors()).ToList();
    }
}

