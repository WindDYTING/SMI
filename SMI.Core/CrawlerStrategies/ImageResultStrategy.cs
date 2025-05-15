using System.Threading.Tasks;
using PuppeteerSharp;
namespace SMI.Core.CrawlerStrategies {
    public class ImageResultStrategy : ICrawlerStrategy<ImageResult> {
        async Task ICrawlerStrategy.GetNewsAsync(IPage page, int notionKind)
        {
            var ret = await GetNewsAsync(page, notionKind);
            Results = ret;
        }

        public ImageResult ResultsOfT => (ImageResult)Results;

        public async Task<ImageResult> GetNewsAsync(IPage page, int notionKind)
        {
            await page.SetViewportAsync(new ViewPortOptions()
            {
                DeviceScaleFactor = 2,
            });
            var stream = await page.ScreenshotStreamAsync(new ScreenshotOptions
            {
                CaptureBeyondViewport = false,
                FullPage = true,
                OptimizeForSpeed = true,
                Type = ScreenshotType.Png,
                BurstMode = true,
                OmitBackground = true
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
