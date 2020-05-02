using System.Net;

namespace BusinessLogic.Models
{
    public class Results<T>
    {
        public T Entity { get; set; }
        public HttpStatusCode HttpStatusCode;
    }
}
