using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using FilmReference.FrontEnd.Handlers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Models;

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
                .GetWhere(g => g.Name.ToLower().Replace(" ", "") == genre.Name.ToLower().Replace(" ", ""))).ToList();

            if (!duplicates.Any()) return false;

            return genre.GenreId <= 0 || duplicates.Any(g => g.GenreId != genre.GenreId);
        }

        public async Task<Results<Genre>> GetGenreById(int id)
        {
            var genre = await _genreRepository.GetById(id);

            return genre == null ?
                new Results<Genre> {HttpStatusCode = HttpStatusCode.NotFound} : 
                new Results<Genre> { Entity = genre, HttpStatusCode = HttpStatusCode.OK};
        }

        public async Task UpdateGenre(Genre genre) =>
            await _genreRepository.Update(genre);

        public async Task SaveGenre(Genre genre)
        {
            await _genreRepository.Add(genre);
            await _genreRepository.Save();
        }
    }
}