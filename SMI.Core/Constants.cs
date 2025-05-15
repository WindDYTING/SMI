namespace SMI.Core;

public static class Constants
{
    public const string NoData = "查無資料";

    public const string Sii = "sii";
    public const string Otc = "otc";
    public const string Rotc = "rotc";
    public const string Pub = "pub";

    public static readonly string[] MarketTypes =
    {
        Sii, Otc, Rotc, Pub
    };

    public const int MaximumOpenInnerPage = 10;
}