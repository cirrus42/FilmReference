using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmReference.DataAccess.DbClasses
{
    [Table("Studio")]
    public partial class StudioEntity : IPicture
    {
        [Key]
        public int StudioId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(500, ErrorMessage = "{0} cannot be more than {1} characters")]
        public string Description { get; set; }
        [Display(Name = "Company Logo")]
        public byte[] Picture { get; set; }

        public ICollection<FilmEntity> Film { get; set; }
    }
}
