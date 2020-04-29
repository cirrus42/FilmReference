namespace Shared.Models
{
    public class FilmPerson
    {
        public int FilmPersonId { get; set; }

        public int FilmId { get; set; }

        public int PersonId { get; set; }

        public Film Film { get; set; }

        public Person Person { get; set; }
    }
}
