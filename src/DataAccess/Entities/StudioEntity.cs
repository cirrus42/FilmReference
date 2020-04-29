using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmReference.DataAccess.Entities
{
    [Table("Studio")]
    public partial class StudioEntity 
    {
        [Key]
        public int StudioId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<FilmEntity> Film { get; set; }
    }
}
