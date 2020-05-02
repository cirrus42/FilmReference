using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public class PersonPagesValues
    {
        public Person Person { get; set; }
        public IEnumerable<Film> Films { get; set; }
    }
}
