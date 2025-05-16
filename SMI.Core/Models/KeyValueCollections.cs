using System.Collections.Generic;

namespace SMI.Core {
    public class KeyValueCollections : Dictionary<string, string>
    {
        public NotionKind NotionKind => (NotionKind)NotionKindValue;

        public int NotionKindValue { get; }

        public KeyValueCollections(int notionKind)
        {
            NotionKindValue = notionKind;
        }
    }
}
