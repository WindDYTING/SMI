using System.Linq;
using System.Threading.Tasks;
using PuppeteerSharp;
using SMI.Models;

namespace SMI.CrawlerStrategies {
    public class KeyValueResultStrategy : ICrawlerStrategy<KeyValueCollections> {

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

        private static async Task<KeyValueCollections> GetNewsCoreAsync(IPage page, int notionKind)
        {
            var dict = new KeyValueCollections();
            var values = await page.QuerySelectorAllAsync(SelectorFactory.GetValueSelector(notionKind));
            var fields = await page.QuerySelectorAllAsync(SelectorFactory.GetFieldSelector(notionKind));
            foreach (var item in fields.Zip(values, (x, y) => (fieldElement: x, valueElement: y)))
            {
                var field = await page.EvaluateFunctionAsync<string>("e => e.textContent", item.fieldElement);
                var value = await page.EvaluateFunctionAsync<string>("e => e.textContent", item.valueElement);
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
