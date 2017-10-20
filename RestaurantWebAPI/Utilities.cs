using System;

namespace RestaurantWebAPI
{
    internal static class Utilities
    {
        internal static string NormalizeJsonPString(string jsonString)
        {
            jsonString = Uri.UnescapeDataString(jsonString);
            var start = jsonString.IndexOf("[", StringComparison.InvariantCulture);
            var end = jsonString.IndexOf("]", StringComparison.InvariantCulture) + 1;
            var length = end - start;
            string normalizedJsonPString;

            try
            {
                normalizedJsonPString = jsonString.Substring(start, length);
            }
            catch (ArgumentOutOfRangeException)
            {
                return jsonString;
            }
            return normalizedJsonPString;
        }
    }
}