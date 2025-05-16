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
            var filterElements = list.Where(kvp => kvp.NotionKind == NotionKind.依證交法第43條之1第1項取得股份公告)
                .SelectMany(kvp => kvp.TakeWhile(x => !x.Key.Equals("3.取得或增減之股數、日期及方式"))
                    .ToDictionary(x => x.Key, x => x.Value))
                .ToDictionary(x => x.Key, x => x.Value);

            return string.Join($"------- {Environment.NewLine}", filterElements.Select(x => x.ToJson().Trim('{', '}')));
        }
    }
}
