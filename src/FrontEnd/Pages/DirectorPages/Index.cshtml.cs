using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;

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

