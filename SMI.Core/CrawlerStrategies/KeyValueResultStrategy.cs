using System.Linq;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace SMI.Core.CrawlerStrategies {
    public class KeyValueResultStrategy : ICrawlerStrategy<KeyValueCollections> {

        public KeyValueResultStrategy(IValueSelector? valueSelector, IFieldSelector? fieldSelector)
        {
            CustomFieldSelector = fieldSelector ?? new DefaultFieldSelector();
            CustomValueSelector = valueSelector ?? new DefaultValueSelector();
        }

        public async Task GetNewsAsync(IPage page, int notionKind) {
            var ret = await GetNewsCoreAsync(page, notionKind);
            Results = ret;
        }

        public KeyValueCollections ResultsOfT => (KeyValueCollections)Results;

        public bool IsValid()
        {
            return ResultsOfT.Any();
        }

        public object Results { get; private set; }
        public IValueSelector CustomValueSelector { get; }
        public IFieldSelector CustomFieldSelector { get; }

        private async Task<KeyValueCollections> GetNewsCoreAsync(IPage page, int notionKind)
        {
            var dict = new KeyValueCollections(notionKind);
            var elementsOfTr = await page.QuerySelectorAllAsync("#div01 > center > table.hasBorder > tbody > tr");

            foreach (var (tr, trIndex) in elementsOfTr
                .Select((e, i) => (e, i)))
            {
                    var fieldSelector = CustomFieldSelector.GetFieldSelectorString(notionKind, trIndex, page);
                    var valueSelector = CustomValueSelector.GetValueSelectorString(notionKind, trIndex, page);
                    var fieldHandle = await page.QuerySelectorAsync(string.Format(fieldSelector, trIndex+1));
                    var valueHandle = await page.QuerySelectorAsync(string.Format(valueSelector, trIndex+1));
                    var field = await page.EvaluateFunctionAsync<string>("e => e.textContent", fieldHandle);
                    var value = await page.EvaluateFunctionAsync<string>("e => e.textContent", valueHandle);
                    field = field.FilterString("\n");
                    value = value.FilterString("\n");
                    dict.TryAdd(field, value);
            }
            return dict;
        }
        
        async Task<KeyValueCollections> ICrawlerStrategy<KeyValueCollections>.GetNewsAsync(IPage page, int notionKind)
        {
            await GetNewsAsync(page, notionKind);
            return ResultsOfT;
        }

    }
}
