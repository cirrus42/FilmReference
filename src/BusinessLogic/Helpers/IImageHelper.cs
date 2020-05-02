using BusinessLogic.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Helpers
{
    public interface IImageHelper
    {
        string ImageSource(byte[] imageContent, bool useDefaultImage = true);
        bool FileTypeOk(IFormFile file, out string errorMessage);
        void AddImageToEntity(IPicture film, IFormFile formFile);
    }
}