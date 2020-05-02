using BusinessLogic.Models;

namespace BusinessLogic.Extensions
{
    public static class StringExtensions
    {
        public static StringValues ConvertToDisplayValue(this string valueToTest, int lengthRequired)
        {

            if (valueToTest == null) return new StringValues();
            
            if (valueToTest.Length > lengthRequired)
                return new StringValues
                {
                    ToolTip = valueToTest,
                    DisplayValue = $"{valueToTest.Substring(0, lengthRequired-3)}..."
                };

            return new StringValues {DisplayValue = valueToTest};
        }

        public static string ReplaceForRadioButton(this string name) =>
            name.Replace(" ", "_").Replace(".", "z");

        public static string BuildFullName(this string firstName, string lastname)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastname))
                return $"{firstName} {lastname}";

            if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastname))
                return firstName;

            if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastname))
                return lastname;

            return "No Name";
        }
    }
}