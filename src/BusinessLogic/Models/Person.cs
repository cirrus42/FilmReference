using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Models
{
    public class Person : IPicture
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "{0} cannot be more than {1} characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName { get; set; }
        [StringLength(200, ErrorMessage = "{0} cannot be more than {1} characters")]
        public string Description { get; set; }
        [Display(Name = "Actor")]
        public bool IsActor { get; set; }
        [Display(Name = "Director")]
        public bool IsDirector { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<Film> Film { get; set; }
        public ICollection<FilmPerson> FilmPerson { get; set; }
    }
}
