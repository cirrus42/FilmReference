using System.Collections.Generic;
using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Extensions;
using FilmReference.FrontEnd.Handlers.Interfaces;
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
                    f.Name.Sanitize() == filmName.Sanitize())).ToList();

            if (!duplicates.Any()) return false;

            return filmId <= 0 || duplicates.Any(film => film.FilmId != filmId);
        }

        public async Task<Results<FilmDetails>> GetFilmById(int id)
        {
            var retrievedFilm =  await _filmRepository.GetAllQueryable()
                .Include(film => film.Director)
                .Include(film => film.Genre)
                .Include(film => film.Studio)
                .Include(film => film.FilmPerson)
                .ThenInclude(filmPerson => filmPerson.Person)
                .FirstOrDefaultAsync(film => film.FilmId == id);

            if (retrievedFilm == null) return new Results<FilmDetails> {HttpStatusCode = HttpStatusCode.NotFound}; 
            
            return  new Results<FilmDetails>
                {
                    Entity = new FilmDetails
                    {
                        Film = retrievedFilm,
                        Actors = retrievedFilm.FilmPerson.Select(filmPerson => filmPerson.Person).OrderBy(person => person.FullName).ToList()
                    },
                    HttpStatusCode = HttpStatusCode.OK
                };
        }

        public async Task<Results<FilmDetails>> GetFilmWithFilmPerson(int id)
        {
            var retrievedFilm = await _filmRepository.GetAllQueryable()
                .Include(film => film.FilmPerson)
                .FirstOrDefaultAsync(film => film.FilmId == id);

            if (retrievedFilm == null) return new Results<FilmDetails> { HttpStatusCode = HttpStatusCode.NotFound };

            return new Results<FilmDetails>
            {
                Entity = new FilmDetails
                {
                    Film = retrievedFilm
                },
                HttpStatusCode = HttpStatusCode.OK
            };
        }

        public async Task UpdateFilm(Film film) =>
            await _filmRepository.Update(film);

        public async Task<IEnumerable<Film>> GetFilms() =>
            await _filmRepository.GetAll();
    }
}