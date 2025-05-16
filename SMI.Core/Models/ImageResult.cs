using System.IO;

namespace SMI.Core {
    public class ImageResult
    {
        public ImageResult(Stream photoStream, int notionKind)
        {
            PhotoStream = photoStream;
            NotionKindValue = notionKind;
        }

        [Newtonsoft.Json.JsonIgnore] public Stream PhotoStream { get; }

        public int NotionKindValue { get; }

        public NotionKind NotionKind => (NotionKind)NotionKindValue;
    }
}
