using FilmReference.DataAccess;
using FilmReference.FrontEnd.Classes.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FilmReference.FrontEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudioController : Controller
    {
        private readonly FilmReferenceContext _context;

        public StudioController(FilmReferenceContext context)
        {
            _context = context;
        }

        // GET: api/Studios/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var imageData = _context.Studio.Find(id).Picture;
            return imageData != null
                ? ImageHelper.ImageSource(imageData)
                : "";
        }
    }
}
