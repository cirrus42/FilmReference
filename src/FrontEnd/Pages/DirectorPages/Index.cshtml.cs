using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;
using FilmReference.FrontEnd.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.DirectorPages
{
    public class IndexModel : FilmReferencePageModel
    {
        private readonly IPersonPagesManager _personPagesManager;
        public IList<Person> PersonList { get; set; }

        public IndexModel( IPersonPagesManager personPagesManager) =>
            _personPagesManager = personPagesManager;
        
        public async Task OnGetAsync() =>
            PersonList = (await _personPagesManager.GetDirectors())
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
    }
}

