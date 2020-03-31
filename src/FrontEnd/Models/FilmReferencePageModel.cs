using FilmReference.DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FilmReference.FrontEnd.Models
{
    public class FilmReferencePageModel : PageModel
    {
        protected FilmReferenceContext _context;
    
        public FilmReferencePageModel(FilmReferenceContext context)
        {
            _context = context;
        }
    }
}
