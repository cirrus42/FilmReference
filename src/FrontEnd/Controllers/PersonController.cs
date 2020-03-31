using FilmReference.DataAccess;
using FilmReference.FrontEnd.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FilmReference.FrontEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly FilmReferenceContext _context;
        private IImageHelper _imageHelper;

        public PersonController(FilmReferenceContext context, IImageHelper imageHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
        }


        // GET <controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var imageData = _context.Person.Find(id).Picture;
            return imageData != null
                ? _imageHelper.ImageSource(imageData)
                : "";
        }
    }
}
