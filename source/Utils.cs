using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SMI.Core;

namespace SMI.Example.Winform {
    internal static class Utils {
        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public static string Process(this IList<KeyValueCollections> list)
        {
            return string.Join($"------- {Environment.NewLine}", list.Select(x => x.ToJson().Trim('{', '}')));
        }
    }
}
