using System.Collections.Generic;
using AutoMapper;
using BusinessLogic.Handlers.Interfaces;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;
using FilmReference.DataAccess.Entities;
using System.Net;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
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
            var studioEntity = _mapper.Map<StudioEntity>(studio);
            if (await _studioHandler.IsDuplicate(studioEntity))
                return false;

            await _studioHandler.SaveStudio(studioEntity);
            return true;
        }

        public async Task<bool> UpdateStudio(Studio studio)
        {
            if (await _studioHandler.IsDuplicate(_mapper.Map<StudioEntity>(studio)))
                return false;

            var studioEntity = await _studioHandler.GetStudioById(studio.Id);

            studioEntity = _mapper.Map(studio, studioEntity);

            await _studioHandler.UpdateStudio(studioEntity);
            return true;
        }

        public async Task<Results<Studio>> GetStudioById(int id)
        {
            var studio = _mapper.Map<Studio>(await _studioHandler.GetStudioById(id));

            return studio == null ?
                new Results<Studio> { HttpStatusCode = HttpStatusCode.NotFound } :
                new Results<Studio> { Entity = studio, HttpStatusCode = HttpStatusCode.OK };
        }

        public async Task<IEnumerable<Studio>> GetStudios() =>
            _mapper.Map<IEnumerable<Studio>>(await _studioHandler.GetStudios());
    }
}