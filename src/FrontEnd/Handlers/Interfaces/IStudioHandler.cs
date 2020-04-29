using FilmReference.DataAccess.Entities;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmReference.FrontEnd.Handlers.Interfaces
{
    internal interface IStudioHandler
    {
        Task<IEnumerable<StudioEntity>> GetStudios();
        Task<bool> IsDuplicate(StudioEntity studio);
        Task SaveStudio(StudioEntity studio);
        Task<Results<StudioEntity>> GetStudioById(int id);
        Task UpdateStudio(StudioEntity studio);
    }
}