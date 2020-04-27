using FilmReference.DataAccess;
using FilmReference.DataAccess.DbClasses;
using FilmReference.FrontEnd.Models;
using Microsoft.AspNetCore.Http;

namespace FilmReference.FrontEnd.Helpers
{
    public interface IImageHelper
    {
        string ImageSource(byte[] imageContent, bool useDefaultImage = true);
        bool FileTypeOk(IFormFile file, out string errorMessage);
        void AddImageToEntity(IPicture film, IFormFile formFile);
    }
}