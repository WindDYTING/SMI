using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PuppeteerSharp;

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

        public static string ToTaiwanDateString(this DateTime dt)
        {
            var year = dt.Year - 1911;
            return $"{year}{dt:MMdd}";
        }

        public static TimeOnly ToTimeOnly(this DateTime dt)
        {
            return new TimeOnly(dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }

        internal static async Task<bool> WaitPageLoadAsync(this IPage page)
        {
            try
            {
                await page.WaitForSelectorAsync("#div01 > center > table.noBorder > tbody > tr:nth-child(1) > td", new WaitForSelectorOptions
                {
                    Timeout = 3500
                });
                await Task.Delay(350);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
