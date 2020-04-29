using FilmReference.DataAccess.Entities;
using FilmReference.FrontEnd.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.GenrePages
{
    public class DetailsModel : PageModel
    {
        private readonly IGenrePagesManager _genrePagesManager;
        public GenreEntity Genre { get; set; }

        public DetailsModel(IGenrePagesManager genrePagesManager) =>
            _genrePagesManager = genrePagesManager;
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _genrePagesManager.GetGenreById(id.Value);

            if (result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();
           
            return Page();
        }
    }
}
