using FilmReference.DataAccess;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.DataAccess.DbClasses;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class IndexModel : PageModel
    {
        public IImageHelper ImageHelper;
        private readonly IStudioHandler _studioHandler;
        public IList<StudioEntity> StudioList { get; set; }
        public IndexModel(IImageHelper imageHelper, IStudioHandler studioHandler)
        {
            ImageHelper = imageHelper;
            _studioHandler = studioHandler;
        }

        public async Task OnGetAsync()
            => StudioList = (await _studioHandler.GetStudios()).ToList();
    }
}