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
            foreach (var collections in list)
            {
                if (collections.NotionKind == NotionKind.依證交法第43條之1第1項取得股份公告)
                {
                    foreach (var kvp in collections.SkipWhile(x => !x.Key.Contains("4.新增或減少之共同取得人")))
                    {
                        collections.Remove(kvp.Key);
                    }
                }
            }

            return string.Join($"------- {Environment.NewLine}", list.Select(x => x.ToJson().Trim('{', '}').Replace("\"", string.Empty)));
        }
    }
}
