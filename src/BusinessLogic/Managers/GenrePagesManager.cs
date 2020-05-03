﻿using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Handlers.Interfaces;
using BusinessLogic.Managers.Interfaces;
using BusinessLogic.Models;
using FilmReference.DataAccess.Entities;

namespace BusinessLogic.Managers
{
    public class GenrePagesManager : IGenrePagesManager
    {
        private readonly IGenreHandler _genreHandler;
        private readonly IMapper _mapper;

        public GenrePagesManager(IGenreHandler genreHandler, IMapper mapper)
        {
            _genreHandler = genreHandler;
            _mapper = mapper;
        }

        public async Task<bool> SaveGenre(Genre genre)
        {
            var genreEntity = _mapper.Map<GenreEntity>(genre);

            if (await _genreHandler.IsDuplicate(genreEntity))
                return false;

            await _genreHandler.SaveGenre(genreEntity);
            return true;
        }

        public async Task<bool> UpdateGenre(Genre genre)
        {
            var genreEntity = _mapper.Map<GenreEntity>(genre);

            if (await _genreHandler.IsDuplicate(genreEntity))
                return false;
            await _genreHandler.UpdateGenre(genreEntity);
            return true;
        }

        public async Task<Results<Genre>> GetGenreById(int id)
        {
            var genreEntity = await _genreHandler.GetGenreById(id);

            return genreEntity == null ?
                new Results<Genre> { HttpStatusCode = HttpStatusCode.NotFound } :
                new Results<Genre> { Entity = _mapper.Map<Genre>(genreEntity), HttpStatusCode = HttpStatusCode.OK };
        }
    }
}