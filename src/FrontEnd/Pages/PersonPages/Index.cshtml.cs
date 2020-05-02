using FilmReference.FrontEnd.Helpers;
using FilmReference.FrontEnd.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.PersonPages
{
    public class IndexModel : PageModel
    {
        public IImageHelper ImageHelper;
        private readonly IPersonPagesManager _personPagesManager;

        public IndexModel(IImageHelper imageHelper, IPersonPagesManager personPagesManager)
        {
            ImageHelper = imageHelper;
            _personPagesManager = personPagesManager;
        }

        public List<Person> ActorList { get;set; }

        public List<string> AtoZ { get; set; }

        public async Task OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                id = "A";

            ActorList = (await _personPagesManager.GetActors(id)).ToList();
          
            AtoZ = new List<string>();

            for (var i = 65; i <= 90; i ++)
                AtoZ.Add(((char)i).ToString());
        }
    }
}