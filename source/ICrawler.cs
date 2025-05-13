using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMI;

public interface ICrawler
{
    Task<IList<Dictionary<string, string>>> GetReportsRange(int selectionValue, string from, string to, int fromPage=0);

    Task<IList<Dictionary<string, string>>> GetSpecifiedMarketTypeReportsRange(int selectionValue, string from, string to, int fromPage,
        string marketType);

    Task<IList<Dictionary<string, string>>> GetReports(int selectionValue, string queryRange, int fromPage=0);
    
    Task<IList<Dictionary<string, string>>> GetSpecifiedMarketTypeReports(int selectionValue, string queryRange, int fromPage, string marketType);
}