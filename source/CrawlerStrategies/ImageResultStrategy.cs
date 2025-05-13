using System.Threading.Tasks;
using PuppeteerSharp;
using SMI.Models;

namespace SMI.CrawlerStrategies {
    public class ImageResultStrategy : ICrawlerStrategy<ImageResult> {
        async Task ICrawlerStrategy.GetNewsAsync(IPage page, int notionKind)
        {
            var ret = await GetNewsAsync(page, notionKind);
            Results = ret;
        }

        public ImageResult ResultsOfT => (ImageResult)Results;

        public async Task<ImageResult> GetNewsAsync(IPage page, int notionKind) {
            var stream = await page.ScreenshotStreamAsync(new ScreenshotOptions()
            {
                FullPage = true
            });
            return new ImageResult(stream);
        }

        public bool IsValid()
        {
            return true;
        }

        public object Results { get; private set; }
    }
}
