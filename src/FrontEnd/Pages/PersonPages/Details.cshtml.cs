using FilmReference.FrontEnd.Helpers;
using FilmReference.FrontEnd.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Models;
using System.Net;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Pages.PersonPages
{
    public class DetailsModel : PageModel
    {
        public IImageHelper ImageHelper;
        private readonly IPersonPagesManager _personPagesManager;
        public PersonPagesValues PersonPagesValues { get; set; }
        public DetailsModel(IImageHelper imageHelper, IPersonPagesManager personPagesManager)
        {
            ImageHelper = imageHelper;
            _personPagesManager = personPagesManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var result = await _personPagesManager.GetPersonDetails(id.Value);

            if (result.HttpStatusCode == HttpStatusCode.NotFound) return NotFound();

            PersonPagesValues = result.Entity;

            return Page();
        }
    }
}
