﻿using FilmReference.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    public interface IGenreHandler
    {
        Task<IEnumerable<GenreEntity>> GetGenres();
        Task<bool> IsDuplicate(GenreEntity genre);
        Task SaveGenre(GenreEntity genre);
        Task<GenreEntity> GetGenreById(int id);
        Task UpdateGenre(GenreEntity genre);
    }
}