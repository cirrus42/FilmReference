using FilmReference.DataAccess.DbClasses;
using FilmReference.DataAccess.Repositories;
using FilmReference.FrontEnd.Handlers.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers
{
    public class FilmHandler : IFilmHandler
    {
        private readonly IGenericRepository<FilmEntity> _filmRepository;

        public FilmHandler(IGenericRepository<FilmEntity> filmRepository) =>
            _filmRepository = filmRepository;

        public async Task SaveFilm(FilmEntity film)
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

        public async Task<FilmEntity> GetFilmById(int id) 
            =>  await _filmRepository.GetAllQueryable()
                .Include(film => film.Director)
                .Include(film => film.Genre)
                .Include(film => film.Studio)
                .Include(film => film.FilmPerson)
                .ThenInclude(filmPerson => filmPerson.Person)
                .FirstOrDefaultAsync(film => film.FilmId == id);

        public async Task<FilmEntity> GetFilmWithFilmPerson(int id) => 
            await _filmRepository.GetAllQueryable()
                .Include(film => film.FilmPerson)
                .FirstOrDefaultAsync(film => film.FilmId == id);

        public async Task UpdateFilm(FilmEntity film) =>
            await _filmRepository.Update(film);

        public async Task<IEnumerable<FilmEntity>> GetFilms() =>
            await _filmRepository.GetAll();
    }
}