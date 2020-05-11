using System.ComponentModel.DataAnnotations.Schema;

namespace FilmReference.DataAccess.Entities
{
    [Table("FilmPerson")]
    public class FilmPersonEntity : BaseEntity
    {
        public int FilmId { get; set; }

        public int PersonId { get; set; }

        public FilmEntity Film { get; set; }

        public PersonEntity Person { get; set; }
    }
}
