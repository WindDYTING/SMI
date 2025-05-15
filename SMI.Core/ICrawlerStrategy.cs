using System.Threading.Tasks;
using PuppeteerSharp;

namespace SMI.Core {
    public interface ICrawlerStrategy
    {
        Task GetNewsAsync(IPage page, int notionKind);

        bool IsValid();

        object Results { get; }
    }

    public interface ICrawlerStrategy<TResult> : ICrawlerStrategy
    {
        Task<TResult> GetNewsAsync(IPage page, int notionKind);

        TResult ResultsOfT { get; }
    }
}
