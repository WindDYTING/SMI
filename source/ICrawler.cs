using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMI;

public interface ICrawler
{
    Task<IList<TResult>> GetReportsRange<TResult>(int selectionValue, string from, string to, int fromPage=0);

    Task<IList<TResult>> GetSpecifiedMarketTypeReportsRange<TResult>(int selectionValue, string from, string to, int fromPage,
        string marketType);

    Task<IList<TResult>> GetReports<TResult>(int selectionValue, string queryRange, int fromPage=0);
    
    Task<IList<TResult>> GetSpecifiedMarketTypeReports<TResult>(int selectionValue, string queryRange, int fromPage, string marketType);
}