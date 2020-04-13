using System.Net;

namespace FilmReference.FrontEnd.Models
{
    public class Results<T>
    {
        public T Entity { get; set; }
        public HttpStatusCode HttpStatusCode;
    }
}
