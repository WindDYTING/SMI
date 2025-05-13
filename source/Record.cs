using System;

namespace SMI;

public record Record(int NotionIndex, string MarketType, Type ResultType)
{
    public int FromPage { get; set; }
    public string StartDate { get; init; }
    public string EndDate { get; init; }
    public string QueryRange { get; init; }

    public Record(int notionIndex, string marketType, string startDate, string endDate, Type resultType) : this(notionIndex, marketType, resultType)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public Record(int notionIndex, string marketType, string queryRange, Type resultType) : this(notionIndex, marketType, resultType)
    {
        QueryRange = queryRange;
    }
}