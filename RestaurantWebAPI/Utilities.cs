using System;

namespace Restaurant.WebAPI
{
    public static class Utilities
    {
        public static string NormalizeJsonPString(string jsonString)
        {
            jsonString = Uri.UnescapeDataString(jsonString);
            var start = jsonString.IndexOf("[", StringComparison.InvariantCulture);
            var end = jsonString.IndexOf("]", StringComparison.InvariantCulture) + 1;
            var length = end - start;
            string normalizedJsonPString;

            try // Is request body JSONP?
            {
                normalizedJsonPString = jsonString.Substring(start, length);
            }
            catch (ArgumentOutOfRangeException) // Request body must be plain old JSON
            {
                return jsonString;
            }
            return normalizedJsonPString;
        }
    }
}