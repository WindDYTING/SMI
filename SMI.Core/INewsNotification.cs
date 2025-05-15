using System.Threading.Tasks;

namespace SMI.Core {

    public interface INewsNotification
    {
        Task SendPhotoAsync(ImageResult image);

        Task SendText(string text);
    }

    public interface INewsNotification<out TClient, out TOptions> : INewsNotification
    {
        TClient Client { get; }

        TOptions Options { get; }
    }
}
