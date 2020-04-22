using FilmReference.DataAccess;
using FilmReference.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmReference.FrontEnd.Handlers.Interfaces;

namespace FilmReference.FrontEnd.Handlers
{
    public class StudioHandler : IStudioHandler
    {
        private readonly IGenericRepository<Studio> _studioRepository;

        public StudioHandler(IGenericRepository<Studio> personRepository) =>
            _studioRepository = personRepository;

        public async Task<IEnumerable<Studio>> GetStudios() =>
            (await _studioRepository.GetAll()).OrderBy(genre => genre.Name);

        public async Task<bool> IsDuplicate(Studio studio)
        {
            var duplicates = (await _studioRepository.GetWhere(s =>
                s.Name.ToLower().Replace(" ", "") == studio.Name.ToLower().Replace(" ", ""))).ToList();

            return duplicates.Any() && duplicates.Any(s => s.StudioId != studio.StudioId);
        }

        public async Task SaveStudio(Studio studio)
        {
            await _studioRepository.Add(studio);
            await _studioRepository.Save();
        }
    }
}