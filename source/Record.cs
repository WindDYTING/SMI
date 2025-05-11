namespace SMI;

public record Record()
{
    public int NotionIndex { get; init; }
    public string MarketType { get; init; }
    public int FromPage { get; set; }
    public string StartDate { get; init; }
    public string EndDate { get; init; }
    public string QueryRange { get; init; }

    public Record(int notionIndex, string marketType, string startDate, string endDate) : this()
    {
        NotionIndex = notionIndex;
        MarketType = marketType;
        StartDate = startDate;
        EndDate = endDate;
    }

    public Record(int notionIndex, string marketType, string queryRange) : this()
    {
        NotionIndex = notionIndex;
        MarketType = marketType;
        QueryRange = queryRange;
    }
}