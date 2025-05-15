using System.IO;

namespace SMI.Core {
    public class ImageResult
    {
        public ImageResult(Stream photoStream)
        {
            PhotoStream = photoStream;
        }

        [Newtonsoft.Json.JsonIgnore] public Stream PhotoStream { get; }

    }
}
