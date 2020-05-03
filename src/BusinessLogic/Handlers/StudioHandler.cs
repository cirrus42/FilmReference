﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Handlers.Interfaces;
using FilmReference.DataAccess.Entities;
using FilmReference.DataAccess.Repositories;

namespace BusinessLogic.Handlers
{
    public class StudioHandler : IStudioHandler
    {
        private readonly IGenericRepository<StudioEntity> _studioRepository;

        public StudioHandler(IGenericRepository<StudioEntity> personRepository) =>
            _studioRepository = personRepository;

        public async Task<IEnumerable<StudioEntity>> GetStudios() =>
            (await _studioRepository.GetAll()).OrderBy(genre => genre.Name);

        public async Task<bool> IsDuplicate(StudioEntity studio)
        {
            var duplicates = (await _studioRepository.GetWhere(s =>
                s.Name.ToLower().Replace(" ", "") == studio.Name.ToLower().Replace(" ", ""))).ToList();

            return duplicates.Any() && duplicates.Any(s => s.StudioId != studio.StudioId);
        }

        public async Task SaveStudio(StudioEntity studio)
        {
            await _studioRepository.Add(studio);
            await _studioRepository.Save();
        }

        public async Task<StudioEntity> GetStudioById(int id) => 
            await _studioRepository.GetById(id);

        public async Task UpdateStudio(StudioEntity studio) =>
                await _studioRepository.Update(studio);
    }
}