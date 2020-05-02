using System;
using System.Collections.Generic;
using System.IO;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public string ImageSource(byte[] imageContent, bool useDefaultImage = true) => 
            imageContent != null
                ? $"data:image/jpg;base64,{Convert.ToBase64String(imageContent)}"
                : useDefaultImage
                    ? "/images/NoCurrentPicture.jpg"
                    : "";

        public bool FileTypeOk(IFormFile file, out string errorMessage)
        {
            var fileType = file.ContentType.Substring(file.ContentType.IndexOf("/", StringComparison.Ordinal) + 1);

            if (new List<string>
            {
                "jpeg",
                "png",
                "gif"
            }.Contains(fileType))
            {
                errorMessage = string.Empty;
                return true;
            }

            errorMessage = "Must be of type .jpg, .gif or .png";
            return false;
        }

        public void AddImageToEntity(IPicture film, IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            formFile.CopyTo(memoryStream);
            film.Picture = memoryStream.ToArray();
        }
    }
}
