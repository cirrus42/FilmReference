using System.Collections.Generic;

namespace FilmReference.FrontEnd.Classes.Helpers
{
    public static class StringHelper
    {
        public static StringValues DisplayValues(string valueToTest, int lengthRequired)
        {
            var stringValues = new StringValues();

            if (valueToTest == null)
            {
                return stringValues;
            }

            if (valueToTest.Length > lengthRequired)
            {
                stringValues.ToolTip = valueToTest;
                stringValues.DisplayValue = $"{valueToTest.Substring(0, 47)}...";
            }
            else
            {
                stringValues.DisplayValue = valueToTest;
            }
            return stringValues;

        }

        public class StringValues
        {
            public string ToolTip { get; set; }

            public string DisplayValue { get; set; }
        }
    }
}
