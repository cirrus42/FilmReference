using FilmReference.DataAccess.Repositories;
using FilmReference.FrontEnd.Handlers.Interfaces;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FilmReference.DataAccess.Entities;

namespace FilmReference.FrontEnd.Handlers
{
    public class GenreHandler : IGenreHandler
    {
        private readonly IGenericRepository<GenreEntity> _genreRepository;

        public GenreHandler(IGenericRepository<GenreEntity> personRepository) =>
            _genreRepository = personRepository;

        public async Task<IEnumerable<GenreEntity>> GetGenres() =>
            (await _genreRepository.GetAll()).OrderBy(genre => genre.Name);

        public async Task<bool> IsDuplicate(GenreEntity genre)
        {
            var duplicates = (await _genreRepository
                .GetWhere(g => g.Name.ToLower().Replace(" ", "") == genre.Name.ToLower().Replace(" ", ""))).ToList();

            if (!duplicates.Any()) return false;

            return genre.GenreId <= 0 || duplicates.Any(g => g.GenreId != genre.GenreId);
        }

        public async Task<GenreEntity> GetGenreById(int id) =>
            await _genreRepository.GetById(id);

        public async Task UpdateGenre(GenreEntity genre) =>
            await _genreRepository.Update(genre);

        public async Task SaveGenre(GenreEntity genre)
        {
            await _genreRepository.Add(genre);
            await _genreRepository.Save();
        }
    }
}