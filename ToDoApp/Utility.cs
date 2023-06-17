namespace ToDoApp
{
    public static class Utility
    {
        public static string GetDecodedString(string base64String)
        {
            byte[] data = Convert.FromBase64String(base64String);
            return System.Text.Encoding.UTF8.GetString(data).Replace("\0", "");
        }

        public static string GetEncodedString(string originalString)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(originalString);

            // Convert bytes to Base64 string
            string base64String = Convert.ToBase64String(data);

            return base64String;
        }
    }
}
