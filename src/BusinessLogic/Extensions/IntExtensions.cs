namespace BusinessLogic.Extensions
{
    public static class IntExtensions
    {
        public static string ShowCorrectRecordText(this int recordCount) =>
            recordCount == 1 ? "" : "s";
    }
}
