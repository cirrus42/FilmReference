using FilmReference.FrontEnd.Helpers;
using FilmReference.FrontEnd.Managers.Interfaces;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.FilmPages
{
    public class DetailsModel : PageModel
    {
        public IImageHelper ImageHelper;
        private readonly IFilmPagesManager _filmPagesManager;
        public FilmDetails FilmDetails { get; set; }

        public DetailsModel(IImageHelper imageHelper, IFilmPagesManager filmPagesManager)
        {
            ImageHelper = imageHelper;
            _filmPagesManager = filmPagesManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _filmPagesManager.GetFilmById(id.Value);

            if(result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            FilmDetails = result.Entity;

            return Page();
        }
    }
}