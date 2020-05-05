using BusinessLogic.Handlers.Interfaces;
using BusinessLogic.Helpers;
using FilmReference.DataAccess.Entities;
using FilmReference.FrontEnd.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class IndexModel : FilmReferencePageModel
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
            => StudioList = (await _studioHandler.GetStudios())
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
    }
}