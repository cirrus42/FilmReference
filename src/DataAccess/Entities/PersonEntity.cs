using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmReference.DataAccess.Entities
{
    [Table("Person")]
    public partial class PersonEntity
    {
        [Key]
        public int PersonId { get; set; }
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
