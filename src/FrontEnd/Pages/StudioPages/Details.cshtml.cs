using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Threading.Tasks;
using BusinessLogic.Helpers;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;

namespace FilmReference.FrontEnd.Pages.StudioPages
{
    public class DetailsModel : PageModel
    {

        public IImageHelper ImageHelper;
        private readonly IStudioPagesManager _studioPagesManager;
        public Studio Studio { get; set; }

        public DetailsModel(IImageHelper imageHelper, IStudioPagesManager studioPagesManager)
        {
            ImageHelper = imageHelper;
            _studioPagesManager = studioPagesManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var results = await _studioPagesManager.GetStudioById(id.Value);

            if (results.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            Studio = results.Entity;
     
            return Page();
        }
    }
}