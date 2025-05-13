using System.IO;
using Newtonsoft.Json;

namespace SMI.Models {
    public class ImageResult
    {
        public ImageResult(Stream photoStream)
        {
            PhotoStream = photoStream;
        }

        [JsonIgnore] public Stream PhotoStream { get; }

    }
}
