using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmReference.DataAccess.Entities
{
    [Table("Person")]
    public class PersonEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; set; }
        public string Description { get; set; }
        public bool IsActor { get; set; }
        public bool IsDirector { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<FilmEntity> Film { get; set; }
        public ICollection<FilmPersonEntity> FilmPerson { get; set; }
    }
}
