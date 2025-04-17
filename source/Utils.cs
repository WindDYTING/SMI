using Newtonsoft.Json;

namespace SMI
{
    internal static class Utils
    {
        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public static string FilterString(this string str, string filterString)
        {
            return str.Replace(filterString, string.Empty);
        }
    }
}
