using System;

namespace SMI.Core.Options {
    public class CrawlerOptions {
        public string GoToUrl { get; private set; }

        public void SetUrl(Uri uri)
        {
            GoToUrl = uri.ToString();
        }
    }
}
