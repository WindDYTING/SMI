namespace SMI.Core {
    public record SubscribeInfo
    {
        public NotionKind ObservationNews { get; init; }

        public ReturnType ReceivedNewsType { get; init; }
    }
}
