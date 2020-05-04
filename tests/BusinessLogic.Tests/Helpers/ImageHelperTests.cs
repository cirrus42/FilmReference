using BusinessLogic.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Text;
using Xunit;

namespace BusinessLogic.Tests.Helpers
{
    public class ImageHelperTests
    {
        private readonly IImageHelper _imageHelper;

        public ImageHelperTests() =>
            _imageHelper = new ImageHelper();

        [Fact]
        public void ImageSourceReturnsCorrectStringForImage()
        {
            const string fakeImage = "FakeImage";
            var imageContent = Encoding.ASCII.GetBytes(fakeImage);
            var base64 = Convert.ToBase64String(imageContent);
            const string outputString = "data:image/jpg;base64,";

            var output = _imageHelper.ImageSource(imageContent, false);

            output.Should().Be($"{outputString}{base64}");
        }

        [Fact]
        public void ImageSourceReturnsCorrectStringDefaultImageTrue()
        {
            const string outputString = "/images/NoCurrentPicture.jpg";

            var output = _imageHelper.ImageSource(null, true);

            output.Should().Be(outputString);
        }

        [Fact]
        public void ImageSourceReturnsCorrectStringDefaultImageNull()
        {
            const string outputString = "/images/NoCurrentPicture.jpg";

            var output = _imageHelper.ImageSource(null);

            output.Should().Be(outputString);
        }

        [Fact]
        public void ImageSourceReturnsCorrectStringDefaultImageFalse()
        {
            var output = _imageHelper.ImageSource(null, false);

            output.Should().BeNullOrEmpty();
        }

        [Theory]
        [InlineData("jpeg", true, "")]
        [InlineData("png", true, "")]
        [InlineData("gif", true, "")]
        [InlineData("bmp", false, "Must be of type .jpg, .gif or .png")]
        [InlineData("txt", false, "Must be of type .jpg, .gif or .png")]
        [InlineData("doc", false, "Must be of type .jpg, .gif or .png")]
        [InlineData("pdf", false, "Must be of type .jpg, .gif or .png")]
        [InlineData("img", false, "Must be of type .jpg, .gif or .png")]
        [InlineData("tiff", false, "Must be of type .jpg, .gif or .png")]
        [InlineData("raw", false, "Must be of type .jpg, .gif or .png")]
        [InlineData("zip", false, "Must be of type .jpg, .gif or .png")]
        [InlineData("rar", false, "Must be of type .jpg, .gif or .png")]
        [InlineData("xls", false, "Must be of type .jpg, .gif or .png")]
        public void FileTypeOkReturnsCorrectBool(string fileType, bool result, string errorMessageResult)
        {
            var formFile = new Mock<IFormFile>();
            var contentType = $"image/{fileType}";

            formFile.Setup(model => model.ContentType).Returns(contentType);

            var output = _imageHelper.FileTypeOk(formFile.Object, out var errorMessage);

            output.Should().Be(result);
            errorMessage.Should().Be(errorMessageResult);
        }
    }
}
