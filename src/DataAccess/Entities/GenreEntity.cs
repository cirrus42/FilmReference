using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmReference.DataAccess.Entities
{
    [Table("Genre")]
    public class GenreEntity : BaseEntity

    { 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public ICollection<FilmEntity> Film { get; set; }
    }
}
