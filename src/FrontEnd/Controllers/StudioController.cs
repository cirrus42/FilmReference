using FilmReference.DataAccess;
using FilmReference.FrontEnd.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FilmReference.FrontEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudioController : Controller
    {
        private readonly FilmReferenceContext _context;
        private IImageHelper _imageHelper;
        public StudioController(FilmReferenceContext context, IImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
        }

        // GET: api/Studios/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var imageData = _context.Studio.Find(id).Picture;
            return imageData != null
                ? _imageHelper.ImageSource(imageData)
                : "";
        }
    }
}
