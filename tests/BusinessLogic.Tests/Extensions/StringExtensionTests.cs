using BusinessLogic.Extensions;
using FluentAssertions;
using Xunit;

namespace BusinessLogic.Tests.Extensions
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("TestString", 11, "TestString", null)]
        [InlineData("TestString", 10, "TestString", null)]
        [InlineData("TestString", 9, "TestSt...", "TestString")]
        [InlineData(null, 9, null, null)]
        public void ConvertToDisplayValueReturnsStringValuesCorrectly(string valueToTest, int lengthRequired,
            string displayValue, string tooltip)
        {
            var output = valueToTest.ConvertToDisplayValue(lengthRequired);
            if (string.IsNullOrWhiteSpace(tooltip))
                output.ToolTip.Should().BeNullOrWhiteSpace();
            else
                output.ToolTip.Should().Be(tooltip);

            output.DisplayValue.Should().Be(displayValue);
        }

        [Theory]
        [InlineData("Test String", "Test_String")]
        [InlineData("Test.String", "TestzString")]
        [InlineData("Test.String one", "TestzString_one")]
        public void ReplaceForRadioButtonReturnsUpdatedString(string name, string updatedName)
        {
            var output = name.ReplaceForRadioButton();
            output.Should().Be(updatedName);
        }

        [Theory]
        [InlineData("First", "Last", "First Last")]
        [InlineData("First", "", "First")]
        [InlineData("First", null, "First")]
        [InlineData("", "Last", "Last")]
        [InlineData(null, "Last", "Last")]
        [InlineData("", "", "No Name")]
        [InlineData(null, null, "No Name")]
        public void BuildFullNameBuildsFullname(string firstname, string lastname, string fullname)
        {
            var output = firstname.BuildFullName(lastname);

            output.Should().Be(fullname);
        }

    }
}
