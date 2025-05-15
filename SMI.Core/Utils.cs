using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace SMI.Core
{
    internal static class Utils
    {
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

        public static IEnumerable<T> FilterType<T>(this IEnumerable<object> source)
        {
            return source.Where(x => x is T).Cast<T>();
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
