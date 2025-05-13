using System.Collections.Generic;

namespace SMI.Models {
    public class KeyValueCollections : Dictionary<string, string> {
        //public Dictionary<string, string> RawData { get; private init; } = new();

        //public bool TryAdd(string key, string value) => RawData.TryAdd(key, value);

        //public static explicit operator KeyValueCollections(Dictionary<string, string> dict) =>
        //    new KeyValueCollections() { RawData = dict };

        //public static implicit operator Dictionary<string, string>(KeyValueCollections self) => self.RawData;
    }
}
