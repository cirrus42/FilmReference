using System.Collections.Generic;
using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmReference.FrontEnd.Handlers
{
    public class FilmHandler : IFilmHandler
    {
        private readonly IGenericRepository<Film> _filmRepository;

        public FilmHandler(IGenericRepository<Film> filmRepository) =>
            _filmRepository = filmRepository;

        public async Task SaveFilm(Film film)
        {
            await _filmRepository.Add(film); 
            await _filmRepository.Save();
        }

        public async Task<bool> IsDuplicate(int filmId, string filmName)
        {
            var duplicates =
                (await _filmRepository.GetWhere(f =>
                    f.Name.ToLower().Replace(" ", "") == filmName.ToLower().Replace(" ", ""))).ToList();

            if (!duplicates.Any()) return false;

            return filmId <= 0 || duplicates.Any(film => film.FilmId != filmId);
        }

        public async Task<Results<FilmDetails>> GetFilmById(int id)
        {
            var film =  await _filmRepository.GetAllQueryable()
                .Include(f => f.Director)
                .Include(f => f.Genre)
                .Include(f => f.Studio)
                .Include(f => f.FilmPerson)
                .ThenInclude(fp => fp.Person)
                .FirstOrDefaultAsync(m => m.FilmId == id);

            if (film == null) return new Results<FilmDetails> {HttpStatusCode = HttpStatusCode.NotFound}; 
            
            return  new Results<FilmDetails>
                {
                    Entity = new FilmDetails
                    {
                        Film = film,
                        Actors = film.FilmPerson.Select(filmPerson => filmPerson.Person).OrderBy(person => person.FullName).ToList()
                    },
                    HttpStatusCode = HttpStatusCode.OK
                };
        }
    }
}
