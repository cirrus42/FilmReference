using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmReference.DataAccess.Entities
{
    [Table("Genre")]
    public partial class GenreEntity
    {
        [Key]
        public int GenreId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<FilmEntity> Film { get; set; }
    }
}
