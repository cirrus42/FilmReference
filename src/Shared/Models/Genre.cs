using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} cannot be more thtan {1} characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(500, ErrorMessage = "{0} cannot be more than {1} characters")]
        public string Description { get; set; }
        public ICollection<Film> Film { get; set; }
    }
}
