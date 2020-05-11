using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmReference.DataAccess.Entities
{
    [Table("Studio")]
    public  class StudioEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<FilmEntity> Film { get; set; }
    }
}
