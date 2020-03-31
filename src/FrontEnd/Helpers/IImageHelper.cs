﻿using Microsoft.AspNetCore.Http;

namespace FilmReference.FrontEnd.Helpers
{
    public interface IImageHelper
    {
        string ImageSource(byte[] imageContent, bool useDefaultImage = true);
        bool FileTypeOk(IFormFile file, out string errorMessage);
    }
}