using System.Net;
using FilmReference.FrontEnd.Handlers.Interfaces;
using FilmReference.FrontEnd.Managers.Interfaces;
using Shared.Models;
using System.Threading.Tasks;
using AutoMapper;
using FilmReference.DataAccess.Entities;

namespace FilmReference.FrontEnd.Managers
{
    public class StudioPagesManager : IStudioPagesManager
    {
        private readonly IStudioHandler _studioHandler;
        private readonly IMapper _mapper;

        public StudioPagesManager(IStudioHandler studioHandler, IMapper mapper)
        {
            _studioHandler = studioHandler;
            _mapper = mapper;
        }

        public async Task<bool> SaveStudio(Studio studio)
        {
            if (await _studioHandler.IsDuplicate(_mapper.Map<StudioEntity>(studio)))
                return false;

            await _studioHandler.SaveStudio(_mapper.Map<StudioEntity>(studio));
            return true;
        }

        public async Task<Results<Studio>> GetStudioById(int id)
        {
            var studio = _mapper.Map<Studio>(await _studioHandler.GetStudioById(id));

            return studio == null ?
                new Results<Studio> { HttpStatusCode = HttpStatusCode.NotFound } :
                new Results<Studio> { Entity = studio, HttpStatusCode = HttpStatusCode.OK };
        }
        
        public async Task<bool> UpdateStudio(Studio studio)
        {
            if (await _studioHandler.IsDuplicate(_mapper.Map<StudioEntity>(studio)))
                return false;
            await _studioHandler.UpdateStudio(_mapper.Map<StudioEntity>(studio));
            return true;
        }
    }
}