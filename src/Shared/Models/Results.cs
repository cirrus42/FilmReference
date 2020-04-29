using System.Net;

namespace Shared.Models
{
    public class Results<T>
    {
        public T Entity { get; set; }
        public HttpStatusCode HttpStatusCode;
    }
}
