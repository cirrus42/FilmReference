using System.ComponentModel.DataAnnotations;

namespace FilmReference.DataAccess.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
