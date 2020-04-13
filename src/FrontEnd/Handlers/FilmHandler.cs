using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
