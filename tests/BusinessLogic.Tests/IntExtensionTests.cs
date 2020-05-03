using BusinessLogic.Extensions;
using FluentAssertions;
using Xunit;

namespace BusinessLogic.Tests
{
    public class IntExtensionTests
    {
        [Theory]
        [InlineData(1, "")]
        [InlineData(2, "s")]
        [InlineData(10, "s")]
        public void ConvertToDisplayValueReturnsStringValuesCorrectly(int recordCount, string outputString)
        {
            var output = recordCount.ShowCorrectRecordText();

            output.Should().Be(outputString);
        }
    }
}
