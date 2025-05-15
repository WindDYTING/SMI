using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SMI.Core.Options;

namespace SMI.Core;

public interface ICrawler
{
    Task<IList<TResult>> GetReportsRange<TResult>(int selectionValue, string from, string to, int fromPage=0);

    Task<IList<TResult>> GetSpecifiedMarketTypeReportsRange<TResult>(int selectionValue, string from, string to, int fromPage,
        string marketType);

    Task<IList<TResult>> GetReports<TResult>(int selectionValue, string queryRange, int fromPage=0);
    
    Task<IList<TResult>> GetSpecifiedMarketTypeReports<TResult>(int selectionValue, string queryRange, int fromPage, string marketType);

    Task<IList<object>> GetSpecifiedMarketTypeReportsRange(int selectionValue, string from, string to, int fromPage,
        string marketType, Type resultType);

    Task<IList<object>> GetSpecifiedMarketTypeReports(int selectionValue, string queryRange, int fromPage, string marketType, Type resultType);
    
    IGuarantor Guarantor { get; set; }
    CrawlerOptions Options { get; }
    Task InitAsync();
}