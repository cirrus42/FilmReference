using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmReference.DataAccess.DbClasses
{
    [Table("FilmPerson")]
    public partial class FilmPersonEntity
    {
        [Key]
        public int FilmPersonId { get; set; }

        public int FilmId { get; set; }

        public int PersonId { get; set; }

        public FilmEntity Film { get; set; }

        public PersonEntity Person { get; set; }
    }
}
