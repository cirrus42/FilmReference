using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Extensions;
using FilmReference.FrontEnd.Handlers.Interfaces;

namespace FilmReference.FrontEnd.Handlers
{
    public class GenreHandler : IGenreHandler
    {
        private readonly IGenericRepository<Genre> _genreRepository;

        public GenreHandler(IGenericRepository<Genre> personRepository) =>
            _genreRepository = personRepository;

        public async Task<IEnumerable<Genre>> GetGenres() =>
            (await _genreRepository.GetAll()).OrderBy(genre => genre.Name);

        public async Task<bool> IsDuplicate(Genre genre)
        {
            var duplicates = (await _genreRepository
                .GetWhere(g => g.Name.Sanitize() == genre.Name.Sanitize())).ToList();
           
            
            throw new System.NotImplementedException();


            //var results = new List<ValidationResult>();

            //var duplicates = _context.Genre
            //    .Where(
            //        g =>
            //            g.Name.ToLower().Replace(" ", "") == Name.ToLower().Replace(" ", ""));
            //if (GenreId > 0) // It's an edit
            //{
            //    duplicates = duplicates.Where(g => g.GenreId != GenreId);
            //}
            //if (duplicates.Any())
            //{
            //    results.Add(new ValidationResult(
            //        "A Genre with this name already exists in the database",
            //        new List<string>
            //        {
            //            nameof(Name)
            //        }));
            //}
            //return results;
        }
    }
}
