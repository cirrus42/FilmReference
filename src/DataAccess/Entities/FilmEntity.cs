using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmReference.DataAccess.Entities
{
    [Table("Film")]
    public  class FilmEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public int StudioId { get; set; }
        public GenreEntity Genre { get; set; }
        public PersonEntity Director { get; set; }
        public StudioEntity Studio { get; set; }
        public ICollection<FilmPersonEntity> FilmPerson { get; set; }
    }
}
