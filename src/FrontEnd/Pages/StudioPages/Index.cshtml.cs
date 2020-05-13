using BusinessLogic.Helpers;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;
using FilmReference.FrontEnd.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class IndexModel : FilmReferencePageModel
    {
        public IImageHelper ImageHelper;
        private readonly IStudioPagesManager _studioPagesManager;
        public IList<Studio> StudioList { get; set; }
        public IndexModel(IImageHelper imageHelper, IStudioPagesManager studioPagesManager)
        {
            ImageHelper = imageHelper;
            _studioPagesManager = studioPagesManager;
        }

        public async Task OnGetAsync() =>
            StudioList = (await _studioPagesManager.GetStudios())
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();
    }
}