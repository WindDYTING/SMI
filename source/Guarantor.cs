using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SMI {
    public class Guarantor : IGuarantor
    {
        private readonly Crawler _crawler;
        private readonly ConcurrentQueue<Record> _records = new();
        private ConcurrentQueue<TaskCompletionSource> _tcsQueue = new();
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        private int _previousFromPage = -1;

        public Guarantor(Crawler crawler)
        {
            _crawler = crawler;
        }

        private async Task RetryGetReportFromRecords()
        {
            if (!_records.TryDequeue(out Record record))
            {
                return;
            }
            if (record.FromPage <= _previousFromPage)
            {
                record.FromPage++;
            }

            Interlocked.Exchange(ref _previousFromPage, record.FromPage);

            IList<object> ret;
            if (!string.IsNullOrEmpty(record.QueryRange))
            {
                ret = await _crawler.GetSpecifiedMarketTypeReports(record.NotionIndex, record.QueryRange, record.FromPage, record.MarketType, record.ResultType);
            }
            else
            {
                ret = await _crawler.GetSpecifiedMarketTypeReportsRange(record.NotionIndex, record.StartDate, record.EndDate, record.FromPage, record.MarketType, record.ResultType);
            }

            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(ret));

            if (_tcsQueue.TryPeek(out TaskCompletionSource tcs))
            {
                tcs.TrySetResult();
                _tcsQueue.TryDequeue(out _);
            }
        }

        public void ClearCache()
        {
            _previousFromPage = -1;
        }

        public async Task WaitGuarantorAsync()
        {
            if (_tcsQueue.TryPeek(out TaskCompletionSource tcs))
            {
                await tcs.Task;
            }
        }

        public TaskCompletionSource EnqueueRecord(Record record)
        {
            if (_records.Count > 3)
            {
                _records.TryDequeue(out _);
            }
            _records.Enqueue(record);
            Task.Factory.StartNew(RetryGetReportFromRecords);

            var tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            _tcsQueue.Enqueue(tcs);
            return tcs;
        }

    }
}
